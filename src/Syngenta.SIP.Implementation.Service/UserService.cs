// ***********************************************************************
// <copyright file="UserService.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// UserService class
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Service.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork;

        /// <summary>
        /// The crypto service
        /// </summary>
        private readonly IApplicationSettingRepository applicationSettings;

        /// <summary>
        /// The crypto service
        /// </summary>
        private readonly ICryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="syngentaSIPUnitOfWork">The syngenta sip unit of work.</param>
        /// <param name="applicationSettings">The application settings.</param>
        /// <param name="cryptoService">The crypto service.</param>
        public UserService(ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork, IApplicationSettingRepository applicationSettings, ICryptoService cryptoService)
        {
            if (this.syngentaSIPUnitOfWork == null)
            {
                this.syngentaSIPUnitOfWork = syngentaSIPUnitOfWork;
            }

            if (this.applicationSettings == null)
            {
                this.applicationSettings = applicationSettings;
            }

            if (this.cryptoService == null)
            {
                this.cryptoService = cryptoService;
            }
        }

        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// returns user by user name
        /// </returns>
        public UserModel GetUserByUserName(string userName)
        {
            UserModel user = this.syngentaSIPUnitOfWork.UserRepository.GetUserByUserName(userName);
            var userRole = user?.CurentRole;
            if (userRole != null)
            {
                user.CurentPlan = this.syngentaSIPUnitOfWork.PlanRepository.GetPlanByRoleIdUserId(userRole.RoleId, user.Id);
            }

            return user;
        }

        /// <summary>
        /// Gets the countries list.
        /// </summary>
        /// <returns>returns countries list</returns>
        public List<CountryModel> GetCountriesList()
        {
            return this.syngentaSIPUnitOfWork.UserRepository.GetCountriesList().OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Saves the last logged in.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns user id
        /// </returns>
        public int SaveLastLoggedIn(int userId)
        {
            return this.syngentaSIPUnitOfWork.UserRepository.SaveLastLoggedIn(userId);
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns user by user id
        /// </returns>
        public UserModel GetUserById(int userId)
        {
            return this.syngentaSIPUnitOfWork.UserRepository.GetUserById(userId);
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns user details
        /// </returns>
        public UserModel GetUserDetails(int userId)
        {
            var user = this.syngentaSIPUnitOfWork.UserRepository.GetUserDetails(userId);
            if (user == null || user.RoleDetails == null || user.RoleDetails.Count <= 0)
            {
                return user;
            }

            if (user.SalaryDetails != null && user.SalaryDetails.Any())
            {
                user.SalaryDetails = user.SalaryDetails.Where(x => x.VisibilityDate <= DateTime.UtcNow.Date || x.VisibilityDate == null).ToList();
            }

            var applicationSetting = this.applicationSettings.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            if (user.SalaryDetails != null && user.SalaryDetails.Any())
            {
                foreach (var salarydetail in user.SalaryDetails)
                {
                    salarydetail.Salary = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, salarydetail.SalaryInDb));
                    ////salarydetail.Salary = Convert.ToDecimal(salarydetail.SalaryInDb);
                }
            }

            var userRole = user.CurentRole;
            if (userRole != null)
            {
                user.CurentPlan = this.syngentaSIPUnitOfWork.PlanRepository.GetPlanByRoleIdUserId(userRole.RoleId, userId);
            }

            return user;
        }

        /// <summary>
        /// Gets the measures.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// returns plans
        /// </returns>
        public PlanViewModel GetMeasures(UserModel user)
        {
            PlanViewModel planViewModel = new PlanViewModel();
            if (user.CurentPlan == null)
            {
                return planViewModel;
            }

            bool isSalaryDetailsExists = true;

            if (user.Country.IsSalaryEditable && user.SalaryDetails.Count == 0)
            {
                isSalaryDetailsExists = false;
            }

            var salariesByQuarter = user.GetSalariesByQuarter(user.CurentPlan.Year, isSalaryDetailsExists);

            var usertarget = this.syngentaSIPUnitOfWork.UserRepository.GetUserTargetsByUserIdPlanId(user.Id, user.CurentPlan.Id).Where(x => x.VisibilityDate != null && x.VisibilityDate <= DateTime.UtcNow.Date).FirstOrDefault();
            int latestSimulationId = 0;
            if (usertarget != null && usertarget.UserSimulations != null)
            {
                latestSimulationId = usertarget.UserSimulations.Select(x => x.Id).OrderByDescending(x => x).FirstOrDefault();
            }

            planViewModel = this.GetPlan(user, salariesByQuarter, usertarget, latestSimulationId);
            return planViewModel;
        }

        /// <summary>
        /// Gets the simulations.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns simulation list</returns>
        public List<PlanViewModel> GetSimulations(UserModel user)
        {
            List<PlanViewModel> simulationList = new List<PlanViewModel>();
            if (user.CurentPlan == null)
            {
                return simulationList;
            }

            var salries = user.GetSalariesByQuarter(user.CurentPlan.Year);
            var usertarget = this.syngentaSIPUnitOfWork.UserRepository.GetUserTargetsByUserIdPlanId(user.Id, user.CurentPlan.Id).Where(x => x.VisibilityDate <= DateTime.UtcNow.Date).FirstOrDefault();
            if (usertarget == null)
            {
                return simulationList;
            }

            foreach (var userTargetSimulation in usertarget.UserSimulations)
            {
                var plan = this.GetPlan(user, salries, usertarget, userTargetSimulation.Id);
                simulationList.Add(plan);
            }

            return simulationList;
        }

        /// <summary>
        /// Saves the salary details.
        /// </summary>
        /// <param name="salaryDetailViewModel">The salary detail view model.</param>
        /// <returns>
        /// returns true if saved else false
        /// </returns>
        public bool SaveSalaryDetails(List<SalaryDetailViewModel> salaryDetailViewModel)
        {
            List<UserSalaryDetailModel> userSalaryDetailList = new List<UserSalaryDetailModel>();
            var applicationSetting = this.applicationSettings.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            if (salaryDetailViewModel != null && salaryDetailViewModel.Count > 0)
            {
                foreach (var salary in salaryDetailViewModel)
                {
                    UserSalaryDetailModel userSalaryDetail = new UserSalaryDetailModel();
                    userSalaryDetail.Id = salary.Id;
                    userSalaryDetail.UserId = salary.UserId;
                    userSalaryDetail.StartDate = salary.StartDate;
                    userSalaryDetail.SalaryInDb = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(salary.BaseSalary));
                    userSalaryDetailList.Add(userSalaryDetail);
                }
            }

            return this.syngentaSIPUnitOfWork.UserRepository.SaveSalaryDetails(userSalaryDetailList);
        }

        /// <summary>
        /// Saves the measures details.
        /// </summary>
        /// <param name="measures">The measures.</param>
        /// <param name="saveSimulation">if set to <c>true</c> [save simulation].</param>
        /// <returns>returns string</returns>
        /// <exception cref="Exception">No Plan Found</exception>
        public string SaveMeasuresDetails(PlanViewModel measures, bool saveSimulation)
        {
            UserModel user = this.GetUserDetails(measures.UserId);
            if (user.CurentPlan == null)
            {
                return "No Plan Found";
            }

            measures.PlanId = user.CurentPlan.Id;
            if (saveSimulation && user != null)
            {
                List<PlanViewModel> simulationList = this.GetSimulations(user);
                if (simulationList.Count > Constants.SimulationCodes.SimulationsCount)
                {
                    return Constants.SimulationCodes.SimulationsMaximumCountReached;
                }
                else if (simulationList.Select(x => x.SimulationName.ToLower()).Equals(measures.SimulationName.ToLower()))
                {
                    return Constants.SimulationCodes.SimulationsNameAlreadyExists;
                }
            }

            this.syngentaSIPUnitOfWork.UserRepository.SaveMeasuresDetails(measures, saveSimulation);
            return Constants.Success;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>
        /// returns all roles
        /// </returns>
        public IQueryable<RoleModel> GetAllRoles()
        {
            return this.syngentaSIPUnitOfWork.UserRepository.GetAllRoles();
        }

        /// <summary>
        /// Deletes the simulation by identifier.
        /// </summary>
        /// <param name="simulationId">The simulation identifier.</param>
        /// <returns>returns code</returns>
        public string DeleteSimulationById(int simulationId)
        {
            this.syngentaSIPUnitOfWork.UserRepository.DeleteSimulationById(simulationId);
            return Constants.SimulationCodes.SimulationDeleted;
        }

        /// <summary>
        /// Gets the payout history.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns view model</returns>
        public List<PlanViewModel> GetPayoutHistory(UserModel user)
        {
            var result = new List<PlanViewModel>();
            List<UserPayoutHistoryModel> payoutHistory = new List<UserPayoutHistoryModel>();

            if (user.Id == 0)
            {
                return result;
            }

            payoutHistory = this.syngentaSIPUnitOfWork.PlanRepository.GetUserPayoutHistory(user.Id);
            if (payoutHistory != null && payoutHistory.Count > 0)
            {
                var yearPayouts = payoutHistory.Where(x => x.VisibilityDate.Date != null && x.VisibilityDate.Date <= DateTime.UtcNow.Date).GroupBy(x => x.Year).Select(g => new { g.Key, Payouts = g.ToList() }).ToList();
                payoutHistory = payoutHistory.Select(x => x).Where(x => x.VisibilityDate.Date != null && x.VisibilityDate.Date <= DateTime.UtcNow.Date).ToList();
                var applicationSetting = this.applicationSettings.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
                foreach (var yearPayout in yearPayouts)
                {
                    PlanViewModel userPayout = new PlanViewModel();
                    foreach (var test in yearPayout.Payouts)
                    {
                        MeassureViewModel meassureViewModel = new MeassureViewModel();

                        meassureViewModel.Name = test.MeasureName;
                        meassureViewModel.Frequency = test.Frequency.Name;
                        if (test.Frequency.Id == (int)Frequency.Annual)
                        {
                            MeasureFrequencyDetailViewModel field = new MeasureFrequencyDetailViewModel();
                            field.Label = (string)FrequencyDetails.Annual.ToString();
                            field.Incentive = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, test.Quarter4));
                            meassureViewModel.Fields.Add(field);
                        }

                        if (test.Frequency.Id == (int)Frequency.HalfYearly)
                        {
                            MeasureFrequencyDetailViewModel field1 = new MeasureFrequencyDetailViewModel();
                            field1.Label = (string)FrequencyDetails.H1.ToString();
                            field1.Incentive = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, test.Quarter2));
                            meassureViewModel.Fields.Add(field1);

                            MeasureFrequencyDetailViewModel field2 = new MeasureFrequencyDetailViewModel();
                            field2.Label = (string)FrequencyDetails.H2.ToString();
                            field2.Incentive = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, test.Quarter4));
                            meassureViewModel.Fields.Add(field2);
                        }

                        if (test.Frequency.Id == (int)Frequency.Quarterly)
                        {
                            MeasureFrequencyDetailViewModel field1 = new MeasureFrequencyDetailViewModel();
                            field1.Label = (string)FrequencyDetails.Q1.ToString();
                            field1.Incentive = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, test.Quarter1));
                            meassureViewModel.Fields.Add(field1);

                            MeasureFrequencyDetailViewModel field2 = new MeasureFrequencyDetailViewModel();
                            field2.Label = (string)FrequencyDetails.Q2.ToString();
                            field2.Incentive = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, test.Quarter2));
                            meassureViewModel.Fields.Add(field2);

                            MeasureFrequencyDetailViewModel field3 = new MeasureFrequencyDetailViewModel();
                            field3.Label = (string)FrequencyDetails.Q3.ToString();
                            field3.Incentive = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, test.Quarter3));
                            meassureViewModel.Fields.Add(field3);

                            MeasureFrequencyDetailViewModel field4 = new MeasureFrequencyDetailViewModel();
                            field4.Label = (string)FrequencyDetails.Q4.ToString();
                            field4.Incentive = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, test.Quarter4));
                            meassureViewModel.Fields.Add(field4);
                        }

                        userPayout.RoleMeassures.Add(meassureViewModel);
                    }

                    userPayout.Year = yearPayout.Key;
                    userPayout.PlanName = yearPayout.Payouts.First().PlanName;
                    result.Add(userPayout);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns>returns list of Languages</returns>
        public List<LanguageModel> GetLanguageList()
        {
            return this.syngentaSIPUnitOfWork.UserRepository.GetLanguageList().OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Saves the basic details.
        /// </summary>
        /// <param name="basicDetailViewModel">The basic detail view model.</param>
        /// <returns>returns true or false </returns>
        public bool SaveBasicDetails(BasicDetailViewModel basicDetailViewModel)
        {
            UserModel userModel = new UserModel();
            if (basicDetailViewModel != null)
            {
                userModel.Id = basicDetailViewModel.UserId;
                userModel.LanguageId = basicDetailViewModel.LanguageID;
            }

            return this.syngentaSIPUnitOfWork.UserRepository.SaveBasicDetails(userModel);
        }

        /// <summary>
        /// Gets the plan.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="salaries">The salaries.</param>
        /// <param name="userTarget">The user target.</param>
        /// <param name="simulationId">The simulation identifier.</param>
        /// <returns>
        /// returns plan view model
        /// </returns>
        private PlanViewModel GetPlan(UserModel user, Dictionary<FrequencyDetails, decimal> salaries, UserTargetModel userTarget, int simulationId)
        {
            var applicationSetting = this.applicationSettings.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            PlanViewModel planViewModel = new PlanViewModel();

            planViewModel = new PlanViewModel()
            {
                PlanId = user.CurentPlan.Id,
                UserId = user.Id,
                CountryId = user.CountryId,
                BussinessUnitId = user.BusinessUnitId,
                RoleId = user.CurentPlan.RoleId,
                Year = user.CurentPlan.Year,
                Role = user.CurentPlan.Name,
                PlanName = user.CurentPlan.Name
            };

            UserSimulationModel userSimulation = null;
            if (userTarget != null && userTarget.UserSimulations.Count > 0)
            {
                planViewModel.SimulationId = simulationId;
                planViewModel.SimulationName = userTarget.UserSimulations.Where(x => x.Id == simulationId).Select(x => x.Name).FirstOrDefault();
                planViewModel.TotalPayout = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, userTarget.UserSimulations.Where(x => x.Id == simulationId).Select(x => x.TotalPayout).FirstOrDefault()));
                userSimulation = userTarget.UserSimulations.Where(x => x.Id == simulationId).FirstOrDefault();
            }

            foreach (var measure in user.CurentPlan.PlanMeasures)
            {
                MeassureViewModel meassureViewModel = new MeassureViewModel();
                meassureViewModel.Id = measure.Id;
                meassureViewModel.Name = measure.MeasureName;
                meassureViewModel.SequenceId = measure.Sequence;
                meassureViewModel.PayoutCurveId = measure.PayoutCurveId;
                meassureViewModel.PayoutTypeId = measure.PayoutTypeId;
                meassureViewModel.PayoutCurve = measure.PayoutCurve;
                meassureViewModel.Frequency = measure.Frequency.Name;
                meassureViewModel.MeasureTargetPercentage = Convert.ToString(measure.MeasureWeightage);
                meassureViewModel.HasGoal = measure.HasGoal;
                meassureViewModel.DataType = measure.DataType;
                meassureViewModel.ValueType = measure.ValueType;
                meassureViewModel.IsKPI = measure.IsKPI;
                UserSimulationMeasureDetailModel userSimulationMeasureDetail = null;
                if (userSimulation != null && userSimulation.UserSimulationMeasureDetails != null)
                {
                    userSimulationMeasureDetail = userSimulation.UserSimulationMeasureDetails.FirstOrDefault(x => x.PlanMeasureId == measure.Id);
                }

                if (userSimulationMeasureDetail != null)
                {
                    meassureViewModel.ActualAchievment = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, userSimulationMeasureDetail.Achievement));
                    meassureViewModel.ActualAchievmentPercentage = userSimulationMeasureDetail.AchievementPercentage;
                    meassureViewModel.YTDCumilativePayout = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, userSimulationMeasureDetail.PayoutAmount));
                }

                var diffInIncentive = 0.0M;
                foreach (var payoutPercentageDetail in measure.PayoutPercentage.PayoutPercentageDetails)
                {
                    MeasureFrequencyDetailViewModel field = new MeasureFrequencyDetailViewModel();
                    field.Id = payoutPercentageDetail.FrequencyDetailId;
                    field.TargetPer = payoutPercentageDetail.PayoutPercentageWeightage;
                    field.Label = payoutPercentageDetail.FrequencyDetail.Name;
                    field.Incentive = salaries[(FrequencyDetails)payoutPercentageDetail.FrequencyDetailId] * (user.CurentPlan.TargetIncentivePercentage / 100) * (measure.MeasureWeightage / 100) * (payoutPercentageDetail.PayoutPercentageWeightage / 100);
                    field.CurrentIncentive = salaries[FrequencyDetails.Annual] * (user.CurentPlan.TargetIncentivePercentage / 100) * (measure.MeasureWeightage / 100) * (payoutPercentageDetail.PayoutPercentageWeightage / 100);
                    if (measure.PayoutCurve.PayoutCurveTypeId == Convert.ToInt32(PayoutCurveType.Linear))
                    {
                        field.AdditionalFields.Add("Expense", 0);
                        field.AdditionalFields.Add("Revenue", 0);
                    }

                    diffInIncentive += field.CurrentIncentive - field.Incentive;

                    if (userTarget != null && userTarget.UserTargetDetails != null)
                    {
                        var userTargetDetail = userTarget.UserTargetDetails.FirstOrDefault(x => x.PlanMeasureId == measure.Id && x.FrequencyDetailId == payoutPercentageDetail.FrequencyDetailId);
                        if (userTargetDetail != null)
                        {
                            field.Goal = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, userTargetDetail.Goal));
                        }
                    }

                    UserSimulationMeasureFrequencyDetailModel userSimulationMeasureFrequencyDetail = null;
                    if (userSimulationMeasureDetail != null && userSimulationMeasureDetail.UserSimulationMeasureFrequencyDetails != null)
                    {
                        userSimulationMeasureFrequencyDetail = userSimulationMeasureDetail.UserSimulationMeasureFrequencyDetails.FirstOrDefault(x => x.FrequencyDetailId == payoutPercentageDetail.FrequencyDetailId);
                    }

                    if (userSimulationMeasureFrequencyDetail != null)
                    {
                        field.ActualAchievment = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, userSimulationMeasureFrequencyDetail.Achievement));
                        field.ActualAchievmentPercentage = userSimulationMeasureFrequencyDetail.AchievementPercentage;
                        field.YTDCumilativePayout = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, userSimulationMeasureFrequencyDetail.PayoutAmount));
                        field.CumulativePercentage = Convert.ToDecimal(userSimulationMeasureFrequencyDetail.CumulativePercentage);
                        field.IsValue = userSimulationMeasureFrequencyDetail.IsValue == null ? false : userSimulationMeasureFrequencyDetail.IsValue;
                        if (!string.IsNullOrWhiteSpace(userSimulationMeasureFrequencyDetail.AdditionalFields))
                        {
                            XmlDocument xdoc = new XmlDocument();
                            xdoc.LoadXml(userSimulationMeasureFrequencyDetail.AdditionalFields);

                            foreach (XmlElement x in xdoc.DocumentElement.ChildNodes)
                            {
                                field.AdditionalFields[x.Name] = Convert.ToInt32(x.InnerText);
                            }
                        }
                    }

                    meassureViewModel.Fields.Add(field);
                }

                if (meassureViewModel.PayoutTypeId == Convert.ToInt32(PayoutType.Cumulative) && meassureViewModel.Fields.Count >= 1)
                {
                    var lastField = meassureViewModel.Fields[meassureViewModel.Fields.Count - 1];
                    lastField.Incentive = lastField.CurrentIncentive + diffInIncentive;
                }

                planViewModel.RoleMeassures.Add(meassureViewModel);
            }

            foreach (var modifier in user.CurentPlan.PlanModifiers)
            {
                ModifierViewModel modifierViewModel = new ModifierViewModel();
                modifierViewModel.Id = modifier.Id;
                modifierViewModel.Name = modifier.ModifierName;
                modifierViewModel.PayoutCurve = modifier.PayoutCurve;
                modifierViewModel.DataType = modifier.DataType;
                modifierViewModel.ValueType = modifier.ValueType;
                if (userSimulation != null)
                {
                    var targetsimulationmodifierdetail = userSimulation.UserSimulationModifierDetails.FirstOrDefault(x => x.PlanModifierId == modifier.Id);
                    if (targetsimulationmodifierdetail != null)
                    {
                        modifierViewModel.ModifierFieldValue = targetsimulationmodifierdetail.ModifierFieldValue;
                        modifierViewModel.ModifierPayout = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, targetsimulationmodifierdetail.ModifierPayout));
                    }
                }

                planViewModel.Modifiers.Add(modifierViewModel);
            }

            planViewModel.UpdateSequence();
            return planViewModel;
        }

        /// <summary>
        /// Gets the plan year.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        public List<int> GetPlanYears(int roleId)
        {
            return this.syngentaSIPUnitOfWork.PlanRepository.GetPlanYears(roleId);
        }


        /// <summary>
        /// Gets the salary details.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public List<SalaryDetailViewModel> GetSalaryDetails(UserModel user)
        {
            List<SalaryDetailViewModel> salaryDetailsList = new List<SalaryDetailViewModel>();
            var role = user.CurentRole;
            foreach (var salarydetail in user.SalaryDetails)
            {
                var salaryDetailViewModel = new SalaryDetailViewModel
                {
                    Id = salarydetail.Id,
                    Role = role?.Role.Name,
                    IncentivePercentage = user.CurentPlan!=null? user.CurentPlan.TargetIncentivePercentage:0,
                    Incentive = salarydetail.Salary * (user.CurentPlan != null ? (user.CurentPlan.TargetIncentivePercentage / 100) : 0.0M),
                    StartDate = salarydetail.StartDate,
                    EndDate = salarydetail.EndDate == null ? this.CurrentYearEndDate() : salarydetail.EndDate,
                    BaseSalary = salarydetail.Salary,
                    UserId = salarydetail.UserId,
                    RoleId = role.RoleId,
                    BusinessId = user.BusinessUnitId,
                    CountryId = user.CountryId
                };
                salaryDetailsList.Add(salaryDetailViewModel);
            }



            List<SalaryDetailViewModel> salaryDetails = new List<SalaryDetailViewModel>();
            bool IsCurrentYear = false;
            bool IsPreviousYear = false;


            foreach (var salarydetail in salaryDetailsList.OrderByDescending(x => x.Id))
            {

                if (salarydetail.StartDate.Year.Equals(DateTime.UtcNow.Year) && salaryDetails.Count() < 2)
                {
                    IsCurrentYear = true;
                    salaryDetails.Add(salarydetail);
                }

                if (salarydetail.StartDate.Year.Equals(DateTime.UtcNow.Year - 1) && salaryDetails.Count() < 2 && IsCurrentYear.Equals(false))
                {
                    IsPreviousYear = true;
                    salaryDetails.Add(salarydetail);
                }
                if (salaryDetails.Count() == 2)
                {
                    break;
                }
            }

            if(salaryDetails.Any() && salaryDetails.Count > 1) 
            {
                if (salaryDetails.First().StartDate != null)
                {
                    salaryDetails.Last().EndDate = Convert.ToDateTime(salaryDetails.First().StartDate).AddDays(-1);
                }
            }          

            return salaryDetails;
        }

        /// <summary>
        /// Currents the year end date.
        /// </summary>
        /// <returns></returns>
        private DateTime CurrentYearEndDate()
        {
            string lastdayofyear = new DateTime(DateTime.Now.Year, 12, 31).ToString();
            return Convert.ToDateTime(lastdayofyear);
        }

        /// <summary>
        /// Gets the editable salary details.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Editable salary details
        /// </returns>
        public SalaryDetailViewModel GetEditableSalaryDetails(UserModel user)
        {
            List<SalaryDetailViewModel> userSalaryHistory = GetSalaryDetails(user);
            SalaryDetailViewModel salaryDetail = new SalaryDetailViewModel();
            var role = user.CurentRole;
            if (userSalaryHistory.Count > 0)
            {
                salaryDetail = userSalaryHistory.OrderByDescending(x => x.Id).FirstOrDefault();
                salaryDetail.StartDate = salaryDetail.StartDate.Equals(this.CurrentYearStartDate()) ? salaryDetail.StartDate : this.CurrentYearStartDate();
                salaryDetail.EndDate = salaryDetail.EndDate.Equals(this.CurrentYearEndDate()) ? salaryDetail.EndDate : this.CurrentYearEndDate();
            }
            else
            {
                salaryDetail.Id = 0;
                salaryDetail.Role = role?.Role.Name;
                salaryDetail.IncentivePercentage = user.CurentPlan.TargetIncentivePercentage;
                salaryDetail.Incentive = 0;
                salaryDetail.StartDate = role.StartDate.Year < DateTime.UtcNow.Year ? this.CurrentYearStartDate() : role.StartDate;
                salaryDetail.EndDate = (role.EndDate == null || role.EndDate < this.CurrentYearStartDate()) ? this.CurrentYearEndDate() : role.EndDate;
                salaryDetail.BaseSalary = 0;
                salaryDetail.UserId = user.Id;
                salaryDetail.RoleId = role.RoleId;
                salaryDetail.BusinessId = user.BusinessUnitId;
                salaryDetail.CountryId = user.CountryId;
                //salaryDetail.VisibilityDate = true;
            }
            return salaryDetail;
        }

        /// <summary>
        /// Currents the year start date.
        /// </summary>
        /// <returns></returns>
        private DateTime CurrentYearStartDate()
        {
            string firstdayofyear = new DateTime(DateTime.Now.Year, 1, 1).ToString();
            return Convert.ToDateTime(firstdayofyear);
        }
    }
}