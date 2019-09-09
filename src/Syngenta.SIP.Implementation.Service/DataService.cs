// ***********************************************************************
// <copyright file="DataService.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Service
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Models;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// DataService class
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Service.IDataService" />
    public class DataService : IDataService
    {
        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork;

        /// <summary>
        /// The crypto service
        /// </summary>
        private readonly ICryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataService" /> class.
        /// </summary>
        /// <param name="syngentaSIPUnitOfWork">The syngenta sip unit of work.</param>
        /// <param name="cryptoService">The crypto service.</param>
        public DataService(ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork, ICryptoService cryptoService)
        {
            if (this.syngentaSIPUnitOfWork == null)
            {
                this.syngentaSIPUnitOfWork = syngentaSIPUnitOfWork;
            }

            if (this.cryptoService == null)
            {
                this.cryptoService = cryptoService;
            }
        }

        /// <summary>
        /// Generates the data file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="id">The identifier.</param>
        public void GenerateDataFile(System.IO.MemoryStream path, int id)
        {
            var excelService = new ExcelService();
            excelService.CreateExcelFile(
                path,
                (DocumentFormat.OpenXml.Packaging.WorkbookPart workBookPart) =>
                {
                    DataTable countryTable = this.syngentaSIPUnitOfWork.DataRepository.GetCountries(id);
                    excelService.AddDataTable(workBookPart, "DataImportCountry", countryTable);

                    DataTable incentivePayout = this.syngentaSIPUnitOfWork.DataRepository.GetIncentivePayout(id);
                    excelService.AddDataTable(workBookPart, "IncentivePayout", incentivePayout);

                    DataTable payoutCurveTable = this.syngentaSIPUnitOfWork.DataRepository.GetPayoutCurves(id);
                    excelService.AddDataTable(workBookPart, "PayoutCurve", payoutCurveTable);

                    DataTable roleTable = this.syngentaSIPUnitOfWork.DataRepository.GetRoles(id);
                    excelService.AddDataTable(workBookPart, "Role", roleTable);

                    DataTable measureTable = this.syngentaSIPUnitOfWork.DataRepository.GetMeasures(id);
                    excelService.AddDataTable(workBookPart, "DataImportMeasure", measureTable);
                });
        }

        /// <summary>
        /// Salaries the import.
        /// </summary>
        /// <param name="excelTable">The excel table.</param>
        /// <returns>returns true or false</returns>
        public bool SalaryImport(DataTable excelTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UserId", typeof(string));
            dataTable.Columns.Add("StartDate", typeof(DateTime));
            dataTable.Columns.Add("EndDate", typeof(DateTime));
            dataTable.Columns.Add("Salary", typeof(string));
            dataTable.Columns.Add("VisibilityDate", typeof(string));
            string encryptedSalary = null;

            var applicationSetting = this.syngentaSIPUnitOfWork.ApplicationSettingRepository.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            if (applicationSetting != null)
            {
                foreach (DataRow row in excelTable.Rows)
                {
                    DataRow dataRow = dataTable.NewRow();
                    DateTime? startdate = this.GetDateTimeFromRow(row, "StartDate");
                    DateTime? enddate = this.GetDateTimeFromRow(row, "EndDate");
                    decimal? salary = this.GetNumberFromRow(row, "Salary");
                    DateTime? visibilityDate = this.GetDateTimeFromRow(row, "VisibilityDate").HasValue ? GetDateTimeFromRow(row, "VisibilityDate") : DateTime.UtcNow;
                    if (salary.HasValue)
                    {
                        encryptedSalary = this.cryptoService.Encrypt(applicationSetting.Value, salary.ToString());
                    }

                    if (startdate.HasValue || salary.HasValue)
                    {
                        dataRow["UserId"] = row["UserId"];
                        dataRow["StartDate"] = startdate;
                        dataRow["EndDate"] = enddate.HasValue ? (object)enddate.Value : DBNull.Value;
                        dataRow["Salary"] = encryptedSalary;
                        dataRow["VisibilityDate"] = visibilityDate;
                        dataTable.Rows.Add(dataRow);
                    }
                }

                this.syngentaSIPUnitOfWork.DataRepository.UploadSalary(dataTable);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Datas the import.
        /// </summary>
        /// <param name="excelTable">The excel table.</param>
        public void ImportCountry(DataTable excelTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("RegionName", typeof(string));
            dataTable.Columns.Add("TerritoryName", typeof(string));
            dataTable.Columns.Add("CountryName", typeof(string));
            dataTable.Columns.Add("DefaultLanguange", typeof(string));
            dataTable.Columns.Add("Currency", typeof(string));
            foreach (DataRow row in excelTable.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                string region = this.GetStringFromRow(row, "RegionName");
                string territory = this.GetStringFromRow(row, "TerritoryName");
                string country = this.GetStringFromRow(row, "CountryName");
                string language = this.GetStringFromRow(row, "DefaultLanguange");
                string currency = this.GetStringFromRow(row, "Currency");

                dataRow["RegionName"] = region;
                dataRow["TerritoryName"] = territory;
                dataRow["CountryName"] = country;
                dataRow["DefaultLanguange"] = language;
                dataRow["Currency"] = currency;
                dataTable.Rows.Add(dataRow);
            }

            this.syngentaSIPUnitOfWork.DataRepository.ImportCountry(dataTable);
        }

        /// <summary>
        /// Datas the import incentive payout.
        /// </summary>
        /// <param name="excelDataTable">The excel data table.</param>
        public void ImportIncentivePayout(DataTable excelDataTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PayoutPercentage", typeof(string));
            dataTable.Columns.Add("FrequencyDetails", typeof(string));
            dataTable.Columns.Add("Percentage", typeof(decimal));
            foreach (DataRow row in excelDataTable.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                string payoutPercentage = this.GetStringFromRow(row, "PayoutPercentage");
                string frequencyDetail = this.GetStringFromRow(row, "FrequencyDetails");
                decimal percentage = this.GetDecimalNumberFromRow(row, "Percentage");

                dataRow["PayoutPercentage"] = payoutPercentage;
                dataRow["FrequencyDetails"] = frequencyDetail;
                dataRow["Percentage"] = percentage;
                dataTable.Rows.Add(dataRow);
            }

            this.syngentaSIPUnitOfWork.DataRepository.ImportIncentivePayout(dataTable);
        }

        /// <summary>
        /// Datas the import payout curve.
        /// </summary>
        /// <param name="excelDataTable">The excel data table.</param>
        public void ImportPayoutCurve(DataTable excelDataTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PayoutCurve", typeof(string));
            dataTable.Columns.Add("SlabId", typeof(int));
            dataTable.Columns.Add("Min", typeof(decimal));
            dataTable.Columns.Add("Max", typeof(decimal));
            dataTable.Columns.Add("Multiplier", typeof(decimal));
            dataTable.Columns.Add("MultiplierType", typeof(string));
            foreach (DataRow row in excelDataTable.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                string payoutCurve = this.GetStringFromRow(row, "PayoutCurve");
                int slabId = this.GetIntegerNumberFromRow(row, "SlabId");
                decimal min = this.GetDecimalNumberFromRow(row, "Min");
                decimal? max = this.GetNumberFromRow(row, "Max");
                decimal multiplier = this.GetDecimalNumberFromRow(row, "Multiplier");
                string multiplierType = this.GetStringFromRow(row, "Multiplier Type");

                dataRow["PayoutCurve"] = payoutCurve;
                dataRow["SlabId"] = slabId;
                dataRow["Min"] = min;
                dataRow["Max"] = max.HasValue ? (object)max.Value : DBNull.Value;
                dataRow["Multiplier"] = multiplier;
                dataRow["MultiplierType"] = multiplierType;
                dataTable.Rows.Add(dataRow);
            }

            this.syngentaSIPUnitOfWork.DataRepository.ImportPayoutCurve(dataTable);
        }

        /// <summary>
        /// Datas the import role.
        /// </summary>
        /// <param name="excelDataTable">The excel data table.</param>
        public void ImportRole(DataTable excelDataTable)
        {
            DataTable roleDataTable = new DataTable();
            roleDataTable.Columns.Add("Country", typeof(string));
            roleDataTable.Columns.Add("BusinessUnit", typeof(string));
            roleDataTable.Columns.Add("Role", typeof(string));
            roleDataTable.Columns.Add("RoleTargetIncentive", typeof(string));
            roleDataTable.Columns.Add("Year", typeof(int));
            roleDataTable.Columns.Add("StartDate", typeof(DateTime));
            roleDataTable.Columns.Add("EndDate", typeof(DateTime));
            roleDataTable.Columns.Add(new DataColumn("VisibilityDate", typeof(DateTime)));
            foreach (DataRow row in excelDataTable.Rows)
            {
                DataRow dataRow = roleDataTable.NewRow();

                string country = this.GetStringFromRow(row, "Country");
                string businessUnit = this.GetStringFromRow(row, "BusinessUnit");
                string role = this.GetStringFromRow(row, "Role");
                decimal roleTargetIncentive = this.GetDecimalNumberFromRow(row, "RoleTargetIncentive");
                Int32? year = this.GetIntegerNumberFromRow(row, "Year");
                DateTime? startDate = this.GetDateTimeFromRow(row, "StartDate");
                DateTime? endDate = this.GetDateTimeFromRow(row, "EndDate");
                DateTime? visibilityDate = this.GetDateTimeFromRow(row, "VisibilityDate").HasValue ? GetDateTimeFromRow(row, "VisibilityDate") : DateTime.UtcNow;

                dataRow["Country"] = country;
                dataRow["BusinessUnit"] = businessUnit;
                dataRow["Role"] = role;
                dataRow["RoleTargetIncentive"] = roleTargetIncentive;
                dataRow["Year"] = year;
                dataRow["StartDate"] = startDate;
                dataRow["EndDate"] = endDate;
                dataRow["VisibilityDate"] = visibilityDate;
                roleDataTable.Rows.Add(dataRow);
            }

            this.syngentaSIPUnitOfWork.DataRepository.ImportRole(roleDataTable);
        }

        /// <summary>
        /// Datas the import measure.
        /// </summary>
        /// <param name="excelDataTable">The excel data table.</param>
        public void ImportMeasure(DataTable excelDataTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Country", typeof(string));
            dataTable.Columns.Add("BusinessUnit", typeof(string));
            dataTable.Columns.Add("Role", typeof(string));
            dataTable.Columns.Add("Measure", typeof(string));
            dataTable.Columns.Add("Frequency", typeof(string));
            dataTable.Columns.Add("MeasureWeightage", typeof(int));
            dataTable.Columns.Add("PayoutCurve", typeof(string));
            dataTable.Columns.Add("PayoutType", typeof(string));
            dataTable.Columns.Add("MeasureSequence", typeof(int));
            dataTable.Columns.Add("IncentivePayout", typeof(string));
            dataTable.Columns.Add("Modifier", typeof(string));
            dataTable.Columns.Add("Goal", typeof(string));
            dataTable.Columns.Add("KPI /Modifier Name", typeof(string));
            dataTable.Columns.Add("KPI/Modifier Value", typeof(string));
            dataTable.Columns.Add("IsKPI", typeof(string));
            dataTable.Columns.Add("Year", typeof(int));
            foreach (DataRow row in excelDataTable.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                string country = this.GetStringFromRow(row, "Country");
                string businessUnit = this.GetStringFromRow(row, "BusinessUnit");
                string role = this.GetStringFromRow(row, "Role");
                int measureSequence = this.GetIntegerNumberFromRow(row, "MeasureSequence");
                string measure = this.GetStringFromRow(row, "Measure");
                string frequency = this.GetStringFromRow(row, "Frequency");
                int measureWeightage = this.GetIntegerNumberFromRow(row, "MeasureWeightage");
                string payoutCurve = this.GetStringFromRow(row, "PayoutCurve");

                string payoutType = this.GetStringFromRow(row, "PayoutType");
                string incentivePayout = this.GetStringFromRow(row, "IncentivePayout");
                string modifier = this.GetStringFromRow(row, "Modifier");
                string goal = this.GetStringFromRow(row, "HasGoal");
                string modifierName = this.GetStringFromRow(row, "KPI /Modifier Name");
                string modifierValue = this.GetStringFromRow(row, "KPI/Modifier Value");
                string kpi = this.GetStringFromRow(row, "IsKPI");
                int year = this.GetIntegerNumberFromRow(row, "Year");
                dataRow["Country"] = country;
                dataRow["BusinessUnit"] = businessUnit;
                dataRow["Role"] = role;
                dataRow["MeasureSequence"] = measureSequence;
                dataRow["PayoutCurve"] = payoutCurve;
                dataRow["Measure"] = measure;
                dataRow["Frequency"] = frequency.Trim();
                dataRow["MeasureWeightage"] = measureWeightage;
                dataRow["PayoutType"] = payoutType;
                dataRow["IncentivePayout"] = incentivePayout;
                dataRow["Modifier"] = modifier;
                dataRow["Goal"] = goal;
                dataRow["KPI /Modifier Name"] = modifierName;
                dataRow["KPI/Modifier Value"] = modifierValue;
                dataRow["IsKPI"] = kpi;
                dataRow["Year"] = year;
                dataTable.Rows.Add(dataRow);
            }

            this.syngentaSIPUnitOfWork.DataRepository.ImportMeasure(dataTable);
        }

        /// <summary>
        /// Datas the import user.
        /// </summary>
        /// <param name="excelDataTable">The excel data table.</param>
        public void ImportUser(DataTable excelDataTable)
        {
            DataTable userDataTable = new DataTable();
            userDataTable.Columns.Add("Country", typeof(string));
            userDataTable.Columns.Add("BusinessUnit", typeof(string));
            userDataTable.Columns.Add("Role", typeof(string));
            userDataTable.Columns.Add("User AD ID", typeof(string));
            userDataTable.Columns.Add("User Name", typeof(string));
            userDataTable.Columns.Add("Employee ID", typeof(string));
            userDataTable.Columns.Add("SIP Plan", typeof(string));
            userDataTable.Columns.Add("LMLoginID", typeof(string));
            userDataTable.Columns.Add("LMID", typeof(string));
            userDataTable.Columns.Add("LMName", typeof(string));
            userDataTable.Columns.Add("Zone", typeof(string));

            foreach (DataRow row in excelDataTable.Rows)
            {
                DataRow dataRow = userDataTable.NewRow();

                string country = this.GetStringFromRow(row, "Country");
                string businessUnit = this.GetStringFromRow(row, "BusinessUnit");
                string role = this.GetStringFromRow(row, "Role");
                string userADID = this.GetStringFromRow(row, "User AD ID");
                string userName = this.GetStringFromRow(row, "User Name");
                string employeeID = this.GetStringFromRow(row, "Employee ID");
                string sipPlan = this.GetStringFromRow(row, "SIP Plan");
                string lmLoginID = this.GetStringFromRow(row, "LM LoginId");
                string lmID = this.GetStringFromRow(row, "LM ID");
                string lmName = this.GetStringFromRow(row, "LM Name");
                string lmZone = this.GetStringFromRow(row, "Zone");

                dataRow["Country"] = country;
                dataRow["BusinessUnit"] = businessUnit;
                dataRow["Role"] = role;
                dataRow["User AD ID"] = userADID;
                dataRow["User Name"] = userName;
                dataRow["Employee ID"] = employeeID;
                dataRow["SIP Plan"] = sipPlan;
                dataRow["LMLoginID"] = lmLoginID;
                dataRow["LMID"] = lmID;
                dataRow["LMName"] = lmName;
                dataRow["Zone"] = lmZone;

                userDataTable.Rows.Add(dataRow);
            }

            this.syngentaSIPUnitOfWork.DataRepository.ImportUser(userDataTable);
        }

        /// <summary>
        /// Generates the user data file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void GenerateUserDataFile(System.IO.MemoryStream path, int countryId)
        {
            var excelService = new ExcelService();
            excelService.CreateExcelFile(
                path,
                (DocumentFormat.OpenXml.Packaging.WorkbookPart workBookPart) =>
                {
                    DataTable userData = this.syngentaSIPUnitOfWork.DataRepository.GetUsersByCountryId(countryId);
                    excelService.AddDataTable(workBookPart, "User", userData);

                    DataTable salaryData = this.syngentaSIPUnitOfWork.DataRepository.GetUserSalaryData(countryId);
                    excelService.AddDataTable(workBookPart, "UserSalary", salaryData);

                    DataTable goalTable = this.syngentaSIPUnitOfWork.DataRepository.GetGoalAmountData(countryId);
                    excelService.AddDataTable(workBookPart, "Goal Amount", goalTable);

                    DataTable payoutHistory = this.syngentaSIPUnitOfWork.DataRepository.GetPayoutHistoryData(countryId);
                    excelService.AddDataTable(workBookPart, "Payout History", payoutHistory);
                });
        }

        /// <summary>
        /// Generates the salary data file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void GenerateSalaryDataFile(System.IO.MemoryStream path)
        {
            var excelService = new ExcelService();
            excelService.CreateExcelFile(
                path,
                (DocumentFormat.OpenXml.Packaging.WorkbookPart workBookPart) =>
                {
                    DataTable countryTable = this.syngentaSIPUnitOfWork.DataRepository.GetUserSalaryData();
                    excelService.AddDataTable(workBookPart, "UserSalaryImport", countryTable);
                });
        }

        /// <summary>
        /// Imports the goal.
        /// </summary>
        /// <param name="excelTable">The excel table.</param>
        public void ImportGoal(DataTable excelTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Country", typeof(string));
            dataTable.Columns.Add("Business Unit", typeof(string));
            dataTable.Columns.Add("Role", typeof(string));
            dataTable.Columns.Add("User AD ID", typeof(string));
            dataTable.Columns.Add("Measure Sequence", typeof(int));
            dataTable.Columns.Add(new DataColumn("Q1", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Q2", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Q3", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Q4", typeof(string)));
            dataTable.Columns.Add(new DataColumn("H1", typeof(string)));
            dataTable.Columns.Add(new DataColumn("H2", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Annual", typeof(string)));
            dataTable.Columns.Add(new DataColumn("VisibilityDate", typeof(DateTime)));
            dataTable.Columns.Add("Year", typeof(int));
            var applicationSettingKey = this.syngentaSIPUnitOfWork.ApplicationSettingRepository.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            if (applicationSettingKey != null)
            {
                foreach (DataRow row in excelTable.Rows)
                {
                    string country = this.GetStringFromRow(row, "Country");
                    string businessUnit = this.GetStringFromRow(row, "Business Unit");
                    string role = this.GetStringFromRow(row, "Role");
                    int year = this.GetIntegerNumberFromRow(row, "Year");
                    string userADID = this.GetStringFromRow(row, "User AD ID");
                    DateTime? visibilityDate = this.GetDateTimeFromRow(row, "VisibilityDate").HasValue ? GetDateTimeFromRow(row, "VisibilityDate") : DateTime.UtcNow.Date;
                    if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(businessUnit) || string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(userADID))
                    {
                        continue;
                    }

                    for (int i = 1; i <= 4; i++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        int measureSquence = this.GetIntegerNumberFromRow(row, "Measure Sequence" + i.ToString());
                        if (measureSquence <= 0)
                        {
                            continue;
                        }

                        var q1 = this.GetNumberFromRow(row, "Q1_" + i.ToString());
                        var q2 = this.GetNumberFromRow(row, "Q2_" + i.ToString());
                        var q3 = this.GetNumberFromRow(row, "Q3_" + i.ToString());
                        var q4 = this.GetNumberFromRow(row, "Q4_" + i.ToString());

                        decimal quarter1 = q1.HasValue ? q1.Value : 0;
                        decimal quarter2 = q2.HasValue ? q2.Value : 0;
                        decimal quarter3 = q3.HasValue ? q3.Value : 0;
                        decimal quarter4 = q4.HasValue ? q4.Value : 0;

                        var h1 = quarter1 + quarter2;
                        var h2 = quarter3 + quarter4;
                        var annual = quarter1 + quarter2 + quarter3 + quarter4;
                        if (annual <= 0)
                        {
                            continue;
                        }

                        dataRow["Country"] = country;
                        dataRow["Business Unit"] = businessUnit;
                        dataRow["Role"] = role;
                        dataRow["Year"] = year;
                        dataRow["User AD ID"] = userADID;

                        dataRow["Measure Sequence"] = measureSquence;

                        dataRow["Q1"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter1.ToString());
                        dataRow["Q2"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter2.ToString());
                        dataRow["Q3"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter3.ToString());
                        dataRow["Q4"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter4.ToString());
                        dataRow["H1"] = this.cryptoService.Encrypt(applicationSettingKey.Value, h1.ToString());
                        dataRow["H2"] = this.cryptoService.Encrypt(applicationSettingKey.Value, h2.ToString());
                        dataRow["Annual"] = this.cryptoService.Encrypt(applicationSettingKey.Value, annual.ToString());
                        dataRow["VisibilityDate"] = visibilityDate;
                        dataTable.Rows.Add(dataRow);
                    }
                }

                this.syngentaSIPUnitOfWork.DataRepository.DataImportGoalMount(dataTable);
            }
        }

        /// <summary>
        /// Generates the goal data file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void GenerateGoalDataFile(System.IO.MemoryStream path)
        {
            var excelService = new ExcelService();
            excelService.CreateExcelFile(
                path,
                (DocumentFormat.OpenXml.Packaging.WorkbookPart workBookPart) =>
                {
                    DataTable goalTable = this.syngentaSIPUnitOfWork.DataRepository.GetGoalAmountData();
                    excelService.AddDataTable(workBookPart, "Goal Amount", goalTable);
                });
        }

        /// <summary>
        /// Imports the payout history.
        /// </summary>
        /// <param name="excelTable">The excel table.</param>
        public void ImportPayoutHistory(DataTable excelTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Year", typeof(string));
            dataTable.Columns.Add("Plan", typeof(string));
            dataTable.Columns.Add("User AD ID", typeof(string));
            dataTable.Columns.Add("Measure Name", typeof(string));
            dataTable.Columns.Add("Freq", typeof(string));
            dataTable.Columns.Add(new DataColumn("Q1", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Q2", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Q3", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Q4", typeof(string)));
            dataTable.Columns.Add(new DataColumn("VisibilityDate", typeof(DateTime)));
            var applicationSettingKey = this.syngentaSIPUnitOfWork.ApplicationSettingRepository.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            if (applicationSettingKey != null)
            {
                foreach (DataRow row in excelTable.Rows)
                {
                    string year = this.GetStringFromRow(row, "Year");
                    string plan = this.GetStringFromRow(row, "Plan");
                    string userADID = this.GetStringFromRow(row, "User AD ID");

                    if (string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(plan) || string.IsNullOrWhiteSpace(userADID))
                    {
                        continue;
                    }

                    for (int i = 1; i <= 4; i++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        string measureName = "Measure Name" + i.ToString();
                        string measureSquenceName = this.GetStringFromRow(row, "Measure Name" + i.ToString());
                        if (!(measureName != null))
                        {
                            continue;
                        }

                        string frequencyName = "Freq" + i.ToString();
                        string measureFreqName = this.GetStringFromRow(row, "Freq" + i.ToString());
                        if (!(frequencyName != null))
                        {
                            continue;
                        }

                        var q1 = this.GetNumberFromRow(row, "Q1_" + i.ToString());
                        var q2 = this.GetNumberFromRow(row, "Q2_" + i.ToString());
                        var q3 = this.GetNumberFromRow(row, "Q3_" + i.ToString());
                        var q4 = this.GetNumberFromRow(row, "Q4_" + i.ToString());
                        DateTime? visibilityDate = this.GetDateTimeFromRow(row, "VisibilityDate").HasValue ? GetDateTimeFromRow(row, "VisibilityDate") : DateTime.UtcNow;

                        decimal quarter1 = q1.HasValue ? q1.Value : 0;
                        decimal quarter2 = q2.HasValue ? q2.Value : 0;
                        decimal quarter3 = q3.HasValue ? q3.Value : 0;
                        decimal quarter4 = q4.HasValue ? q4.Value : 0;

                        dataRow["Year"] = year;
                        dataRow["Plan"] = plan;
                        dataRow["Measure Name"] = measureSquenceName;
                        dataRow["User AD ID"] = userADID;
                        dataRow["Freq"] = measureFreqName;
                        dataRow["Q1"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter1.ToString());
                        dataRow["Q2"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter2.ToString());
                        dataRow["Q3"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter3.ToString());
                        dataRow["Q4"] = this.cryptoService.Encrypt(applicationSettingKey.Value, quarter4.ToString());
                        dataRow["VisibilityDate"] = visibilityDate;
                        dataTable.Rows.Add(dataRow);
                    }
                }

                this.syngentaSIPUnitOfWork.DataRepository.DataImportPayoutHistory(dataTable);
            }
        }

        /// <summary>
        /// Gets the date time from row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>
        /// date time if parsed success fully, null otherwise
        /// </returns>
        private DateTime? GetDateTimeFromRow(DataRow row, string columnName)
        {
            try
            {
                DateTime result = new DateTime();
                bool isParsed = DateTime.TryParse(row[columnName].ToString(), out result);
                if (isParsed)
                {
                    return result;
                }

                double dates = 0;
                isParsed = double.TryParse(row[columnName].ToString(), out dates);
                if (isParsed && dates > 0)
                {
                    return DateTime.FromOADate(dates);
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        /// <summary>
        /// Gets the number from row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>
        /// decimal if parsed success fully, null otherwise
        /// </returns>
        private decimal? GetNumberFromRow(DataRow row, string columnName)
        {
            try
            {
                decimal result = 0;
                bool isParsed = decimal.TryParse(row[columnName].ToString(), out result);
                if (isParsed)
                {
                    return result;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        /// <summary>
        /// Gets the decimal number from row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns> Return decimal </returns>
        private decimal GetDecimalNumberFromRow(DataRow row, string columnName)
        {
            try
            {
                decimal result = 0;
                bool isParsed = decimal.TryParse(row[columnName].ToString(), out result);
                if (isParsed)
                {
                    return result;
                }
            }
            catch (Exception)
            {
            }

            return 0;
        }

        /// <summary>
        /// Gets the integer number from row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>returns integer value</returns>
        private int GetIntegerNumberFromRow(DataRow row, string columnName)
        {
            try
            {
                int result = 0;
                bool isParsed = int.TryParse(row[columnName].ToString(), out result);
                if (isParsed)
                {
                    return result;
                }
            }
            catch (Exception)
            {
            }

            return 0;
        }

        /// <summary>
        /// Gets the string from row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>returns string </returns>
        private string GetStringFromRow(DataRow row, string columnName)
        {
            string isParsed = null;
            try
            {
                isParsed = Convert.ToString(row[columnName]).Trim();
            }
            catch (Exception)
            {
            }

            return isParsed;
        }
    }
}
