// ***********************************************************************
// <copyright file="UserRepository.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// UserRepository class
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Repository.IUserRepository" />
    public class UserRepository : IUserRepository
    {

        /// <summary>
        /// The syngenta context
        /// </summary>
        private readonly ISyngentaSIPContext syngentaSIPContext;

        /// <summary>
        /// The application settings
        /// </summary>
        private readonly IApplicationSettingRepository applicationSettings;

        /// <summary>
        /// The icrypto service
        /// </summary>
        private readonly ICryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="syngentaSIPContext">The syngenta sip context.</param>
        /// <param name="cryptoService">The crypto service.</param>
        /// <param name="applicationSettingRepository">The application setting repository.</param>
        public UserRepository(ISyngentaSIPContext syngentaSIPContext,ICryptoService cryptoService,IApplicationSettingRepository applicationSetting)
        {
            this.syngentaSIPContext = syngentaSIPContext;
            this.cryptoService = cryptoService;
            this.applicationSettings = applicationSetting;
        }

        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>returns user by name</returns>
        public UserModel GetUserByUserName(string userName)
        {
            var user = this.syngentaSIPContext.User
                .Where(x => (x.LoginID ?? string.Empty).ToLower().Trim() == userName.ToLower().Trim() || (x.Email ?? string.Empty).ToLower().Trim() == userName.ToLower().Trim())
                .FirstOrDefault();

            if(user == null)
            {
                return user;
            }

            return this.syngentaSIPContext.User.Include(x => x.Country).Include(x => x.RoleDetails).Include(x => x.Permissions).Where(r => r.Id == user.Id).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns user by id</returns>
        public UserModel GetUserById(int id)
        {
            return this.syngentaSIPContext.User.Where(r => r.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user by login identifier.
        /// </summary>
        /// <param name="loginId">The login identifier.</param>
        /// <returns>returns user model</returns>
        public UserModel GetUserByLoginId(string loginId)
        {
            return this.syngentaSIPContext.User.Where(r => r.LoginID == loginId).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>get user details by id</returns>
        public UserModel GetUserDetails(int userId)
        {
            return this.syngentaSIPContext.User
                .Include(x => x.BusinessUnit)
                .Include(x => x.Country)
                .Include(x => x.Country.Currency)
                .Include(x => x.City)
                .Include(x => x.RoleDetails.Select(y => y.Role))
                .Include(x => x.SalaryDetails)
                .Include(x => x.Language)
                .Where(r => r.Id == userId).FirstOrDefault();
        }

        /// <summary>
        /// Saves the salary details.
        /// </summary>
        /// <param name="userSalaryDetailList">The user salary detail list.</param>
        /// <returns>
        /// save salary details to data base
        /// </returns>
        public bool SaveSalaryDetails(List<UserSalaryDetailModel> userSalaryDetailList)
        {
            foreach (var salarydetail in userSalaryDetailList)
            {
                var userSalary = this.syngentaSIPContext.UserSalaryDetails.FirstOrDefault(x => x.Id == salarydetail.Id);
                if (userSalary != null)
                {
                    userSalary.StartDate = DateTime.UtcNow.Date;
                    userSalary.LastUpdated = DateTime.UtcNow;
                    userSalary.SalaryInDb = salarydetail.SalaryInDb;
                    //userSalary.VisibilityDate = DateTime.UtcNow.Date;
                }
                else
                {
                    userSalary = new UserSalaryDetailModel
                    {
                        SalaryInDb = salarydetail.SalaryInDb,
                        UserId = salarydetail.UserId,
                        StartDate = DateTime.UtcNow.Date,
                        //VisibilityDate = DateTime.UtcNow.Date,
                        LastUpdated = DateTime.UtcNow
                    };

                    this.syngentaSIPContext.UserSalaryDetails.Add(userSalary);
                }
                this.syngentaSIPContext.Save();
            }


            return true;
        }

        /// <summary>
        /// Saves the last logged in.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>saves login data to database</returns>
        public int SaveLastLoggedIn(int userId)
        {
            var doesUserExists = this.syngentaSIPContext.User.Where(x => x.Id == userId).FirstOrDefault();
            if (doesUserExists != null)
            {
                doesUserExists.LastLogin = DateTime.Now;
                this.syngentaSIPContext.Save();
                return doesUserExists.Id;
            }
            else
            {
                return (int)DBSaveExceptions.DeletedByOtherUser;
            }
        }

        /// <summary>
        /// Saves the measures details.
        /// </summary>
        /// <param name="planViewModel">The plan view model.</param>
        /// <param name="saveSimulation">if set to <c>true</c> [save simulation].</param>
        public void SaveMeasuresDetails(PlanViewModel planViewModel, bool saveSimulation)
        {
            var  applicationSetting = this.applicationSettings.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            var userTarget = this.syngentaSIPContext.UserTargets
                .Include(x => x.UserTargetDetails)
                .Where(x => x.PlanId == planViewModel.PlanId && x.UserId == planViewModel.UserId).OrderByDescending(x => x.Id).FirstOrDefault();
            if (userTarget == null)
            {
                userTarget = new UserTargetModel();
                userTarget.PlanId = planViewModel.PlanId;
                userTarget.UserId = planViewModel.UserId;

                userTarget.UserTargetDetails = new List<UserTargetDetailModel>();
                foreach (var meassure in planViewModel.RoleMeassures)
                {
                    foreach (var frequencyDetail in meassure.Fields)
                    {
                        var userTargetDetail = new UserTargetDetailModel();
                        userTargetDetail.PlanMeasureId = meassure.Id;
                        userTargetDetail.FrequencyDetailId = frequencyDetail.Id;
                        userTargetDetail.Incentive = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(frequencyDetail.Incentive));
                        userTargetDetail.Goal = this.cryptoService.Encrypt(applicationSetting.Value,Convert.ToString(frequencyDetail.Goal));
                        userTarget.UserTargetDetails.Add(userTargetDetail);
                    }
                }

                this.syngentaSIPContext.UserTargets.Add(userTarget);
            }
            else
            {
                foreach (var meassure in planViewModel.RoleMeassures)
                {
                    foreach (var frequencyDetail in meassure.Fields)
                    {
                        var userTargetDetail = userTarget.UserTargetDetails.Where(x => x.PlanMeasureId == meassure.Id && x.FrequencyDetailId == frequencyDetail.Id).FirstOrDefault();
                        if (userTargetDetail == null)
                        {
                            userTargetDetail = new UserTargetDetailModel();
                            userTargetDetail.PlanMeasureId = meassure.Id;
                            userTargetDetail.FrequencyDetailId = frequencyDetail.Id;
                            userTargetDetail.Incentive = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(frequencyDetail.Incentive));
                            userTargetDetail.Goal = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(frequencyDetail.Goal));
                            userTarget.UserTargetDetails.Add(userTargetDetail);
                        }
                        else
                        {
                            userTargetDetail.Incentive = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(frequencyDetail.Incentive));
                            userTargetDetail.Goal = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(frequencyDetail.Goal));
                            userTarget.UserTargetDetails.Add(userTargetDetail);
                        }
                    }
                }
            }

            if (saveSimulation)
            {
                var userSimulation = new UserSimulationModel();
                userSimulation.Name = planViewModel.SimulationName;
                userSimulation.TotalPayout = this.cryptoService.Encrypt(applicationSetting.Value,Convert.ToString(planViewModel.TotalPayout));
                userSimulation.UserSimulationMeasureDetails = new List<UserSimulationMeasureDetailModel>();
                foreach (var meassure in planViewModel.RoleMeassures)
                {
                    var userSimulationMeasureDetail = new UserSimulationMeasureDetailModel();
                    userSimulationMeasureDetail.PlanMeasureId = meassure.Id;
                    userSimulationMeasureDetail.Achievement = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(meassure.ActualAchievment));
                    userSimulationMeasureDetail.AchievementPercentage = meassure.ActualAchievmentPercentage;
                    userSimulationMeasureDetail.PayoutAmount = this.cryptoService.Encrypt(applicationSetting.Value,Convert.ToString(meassure.YTDCumilativePayout));

                    foreach (var frequencyDetail in meassure.Fields)
                    {
                        var userSimulationMeasureFrequencyDetail = new UserSimulationMeasureFrequencyDetailModel();

                        userSimulationMeasureFrequencyDetail.FrequencyDetailId = frequencyDetail.Id;
                        userSimulationMeasureFrequencyDetail.PayoutAmount = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(frequencyDetail.YTDCumilativePayout));
                        userSimulationMeasureFrequencyDetail.Achievement = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(frequencyDetail.ActualAchievment));
                        userSimulationMeasureFrequencyDetail.AchievementPercentage = frequencyDetail.ActualAchievmentPercentage;
                        userSimulationMeasureFrequencyDetail.CumulativePercentage = frequencyDetail.CumulativePercentage;
                        userSimulationMeasureFrequencyDetail.IsValue = frequencyDetail.IsValue;
                        if (frequencyDetail.AdditionalFields != null && frequencyDetail.AdditionalFields.Keys.Count > 0)
                        {
                            XElement xelement = new XElement("root");
                            foreach (var additionalField in frequencyDetail.AdditionalFields)
                            {
                                xelement.Add(new XElement(additionalField.Key, additionalField.Value));
                            }

                            userSimulationMeasureFrequencyDetail.AdditionalFields = xelement.ToString();
                        }

                        userSimulationMeasureDetail.UserSimulationMeasureFrequencyDetails.Add(userSimulationMeasureFrequencyDetail);
                    }

                    userSimulation.UserSimulationMeasureDetails.Add(userSimulationMeasureDetail);
                }

                foreach (var modifier in planViewModel.Modifiers)
                {
                    var userSimulationModifierDetail = new UserSimulationModifierDetailModel();
                    userSimulationModifierDetail.ModifierFieldValue = modifier.ModifierFieldValue;
                    userSimulationModifierDetail.ModifierPayout = this.cryptoService.Encrypt(applicationSetting.Value, Convert.ToString(modifier.ModifierPayout));
                    userSimulationModifierDetail.PlanModifierId = modifier.Id;
                    userSimulation.UserSimulationModifierDetails.Add(userSimulationModifierDetail);
                }

                userTarget.UserSimulations.Add(userSimulation);
            }

            this.syngentaSIPContext.Save();
        }

        /// <summary>
        /// Gets the user targets by user identifier plan identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="planId">The plan identifier.</param>
        /// <returns>get user targets by User id and Plan id</returns>
        public List<UserTargetModel> GetUserTargetsByUserIdPlanId(int userId, int planId)
        {
            return this.syngentaSIPContext.UserTargets
                .Include(x => x.UserTargetDetails)
                .Include(x => x.UserSimulations.Select(y => y.UserSimulationMeasureDetails.Select(z => z.UserSimulationMeasureFrequencyDetails)))
                .Include(x => x.UserSimulations.Select(y => y.UserSimulationModifierDetails))
                .Where(r => r.UserId == userId && r.PlanId == planId)
                .OrderByDescending(x => x.Id).ToList();
        }

        /// <summary>
        /// Gets the roles list.
        /// </summary>
        /// <returns>
        /// returns roles list
        /// </returns>
        public List<RoleModel> GetRolesList()
        {
            return this.syngentaSIPContext.Role.ToList();
        }

        /// <summary>
        /// Gets the countries list.
        /// </summary>
        /// <returns>
        /// returns countries list
        /// </returns>
        public List<CountryModel> GetCountriesList()
        {
            return this.syngentaSIPContext.Country.ToList();

        }

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns> Return list of languages</returns>
        public List<LanguageModel> GetLanguageList()
        {
            return this.syngentaSIPContext.Languages.ToList();
        }

        /// <summary>
        /// Gets the business list.
        /// </summary>
        /// <returns>
        /// returns business unit list
        /// </returns>
        public List<BusinessUnitModel> GetBussinessList()
        {
            return this.syngentaSIPContext.BusinessUnit.ToList();
        }

        /// <summary>
        /// Changes the user role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="bussinessunitid">The business unit id.</param>
        /// <param name="countryid">The country id.</param>
        /// <param name="roleid">The role id.</param>
        public void ChangeUserRole(UserModel user, int bussinessunitid, int countryid, int roleid)
        {
            if (user.RoleDetails.Count > 0)
            {
                user.CountryId = countryid;
                user.BusinessUnitId = bussinessunitid;
                user.LanguageId = this.syngentaSIPContext.Country.Where(x => x.Id == countryid).Select(y => y.LanguageId).FirstOrDefault();
                //foreach (var roledetail in user.RoleDetails)
                //{
                //    roledetail.RoleId = roleid;
                //}
            }
            else
            {
                UserRoleDetailModel userRoleDetailModel = new UserRoleDetailModel();
                userRoleDetailModel.UserId = user.Id;
                userRoleDetailModel.RoleId = roleid;
                userRoleDetailModel.StartDate = System.DateTime.Now;
                userRoleDetailModel.EndDate = System.DateTime.Now;
                user.RoleDetails.Add(userRoleDetailModel);
            }

            this.syngentaSIPContext.Save();
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>
        /// returns all roles
        /// </returns>
        public IQueryable<RoleModel> GetAllRoles()
        {
            return this.syngentaSIPContext.Role;
        }

        /// <summary>
        /// Deletes the simulation by identifier.
        /// </summary>
        /// <param name="simulationId">The simulation identifier.</param>
        public void DeleteSimulationById(int simulationId)
        {
            var simulation = this.syngentaSIPContext.UserSimulations
                .Include("UserSimulationMeasureDetails")
                .Include("UserSimulationMeasureDetails.UserSimulationMeasureFrequencyDetails")
                .Include("UserSimulationModifierDetails")
                .Where(x => x.Id == simulationId).FirstOrDefault();

            if (simulation != null)
            {
                if (simulation.UserSimulationMeasureDetails.Count > 0)
                {
                    var userSimulationMeasureDetails = simulation.UserSimulationMeasureDetails.ToList();
                    foreach (var simulationMeasureDetail in userSimulationMeasureDetails)
                    {
                        var simulationMeasureFrequencyDetails = simulationMeasureDetail.UserSimulationMeasureFrequencyDetails.ToList();
                        foreach (var simulationMeasureFrequencyDetail in simulationMeasureFrequencyDetails)
                        {
                            this.syngentaSIPContext.UserSimulationMeasureFrequencyDetails.Remove(simulationMeasureFrequencyDetail);
                        }

                        this.syngentaSIPContext.UserSimulationMeasureDetails.Remove(simulationMeasureDetail);
                    }
                }

                if (simulation.UserSimulationModifierDetails.Count > 0)
                {
                    var userSimulationModifierDetails = simulation.UserSimulationModifierDetails.ToList();
                    foreach (var simulationModifierDetail in userSimulationModifierDetails)
                    {
                        this.syngentaSIPContext.UserSimulationModifierDetails.Remove(simulationModifierDetail);
                    }
                }

                this.syngentaSIPContext.UserSimulations.Remove(simulation);
            }

            this.syngentaSIPContext.Save();
        }

        /// <summary>
        /// Saves the basic details.
        /// </summary>
        /// <param name="userModel">The basic detail view model.</param>
        /// <returns>returns true or false</returns>
        public bool SaveBasicDetails(UserModel userModel)
        {
            var userBasicDetails = this.syngentaSIPContext.User.FirstOrDefault(x => x.Id == userModel.Id);
            if (userBasicDetails != null)
            {
                userBasicDetails.LanguageId = userModel.LanguageId;
                this.syngentaSIPContext.Save();
            }

            return true;
        }

        /// <summary>
        /// Gets the years list.
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, int> GetYearsList()
        {
            var yearList = new Dictionary<int, int>();
            var currentYear = DateTime.Today.Year;

            for (int i = 0; i >= 0; i--)
            {
                yearList.Add((currentYear - i), (currentYear - i));
            }

            return yearList;
        }
    }
}