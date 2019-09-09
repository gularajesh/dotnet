// ***********************************************************************
// <copyright file="DataRepository.cs" company="Syngenta">
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
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using MoreLinq;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// DataRepository class
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Repository.IDataRepository" />
    public class DataRepository : IDataRepository
    {
        /// <summary>
        /// The syngenta context
        /// </summary>
        private readonly ISyngentaSIPContext syngentaSIPContext;

        /// <summary>
        /// The syngenta sip security context
        /// </summary>
        private readonly ISyngentaSIPSecurityContext syngentaSIPSecurityContext;

        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork;

        /// <summary>
        /// The crypto service
        /// </summary>
        private readonly ICryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRepository" /> class.
        /// </summary>
        /// <param name="syngentaSIPContext">The syngenta sip context.</param>
        /// <param name="syngentaSIPSecurityContext">The syngenta sip security context.</param>
        /// <param name="cryptoService">The crypto service.</param>
        /// <param name="syngentaSIPUnitOfWork">The syngenta sip unit of work.</param>
        public DataRepository(ISyngentaSIPContext syngentaSIPContext, ISyngentaSIPSecurityContext syngentaSIPSecurityContext, ICryptoService cryptoService, ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork)
        {
            this.syngentaSIPContext = syngentaSIPContext;
            this.syngentaSIPSecurityContext = syngentaSIPSecurityContext;
            this.cryptoService = cryptoService;
            this.syngentaSIPUnitOfWork = syngentaSIPUnitOfWork;
        }

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns countries
        /// </returns>
        public DataTable GetCountries(int countryId)
        {
            return this.syngentaSIPContext.ExecuteProcedure("Proc_GetCountry", new SqlParameter("@CountryId", countryId));
        }

        /// <summary>
        /// Gets the payout curves.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns payout curves
        /// </returns>
        public DataTable GetPayoutCurves(int countryId)
        {
            return this.syngentaSIPContext.ExecuteProcedure("Proc_GetPayoutCurve", new SqlParameter("@CountryId", countryId));
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns roles
        /// </returns>
        public DataTable GetRoles(int countryId)
        {
            return this.syngentaSIPContext.ExecuteProcedure("Proc_GetRoles", new SqlParameter("@CountryId", countryId));
        }

        /// <summary>
        /// Gets the incentive payout.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns payouts
        /// </returns>
        public DataTable GetIncentivePayout(int countryId)
        {
            return this.syngentaSIPContext.ExecuteProcedure("Proc_GetIncentivePayout", new SqlParameter("@CountryId", countryId));
        }

        /// <summary>
        /// Uploads the salary.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void UploadSalary(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportSalary", "@Salary");
        }

        /// <summary>
        /// Gets the measures.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns measures
        /// </returns>
        public DataTable GetMeasures(int countryId)
        {
            return this.syngentaSIPContext.ExecuteProcedure("Proc_GetMeasures", new SqlParameter("@CountryId", countryId));
        }

        /// <summary>
        /// Datas the import country.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void ImportCountry(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportCountry", "@DataImportCountry");
        }

        /// <summary>
        /// Datas the import incentive payout.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void ImportIncentivePayout(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportIncentivePercentage", "@IncentivePayout");
        }

        /// <summary>
        /// Payouts the curve.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void ImportPayoutCurve(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportPayoutCurve", "@PayoutCurve");
        }

        /// <summary>
        /// Datas the import role.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void ImportRole(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportRole", "@tempRole");
        }

        /// <summary>
        /// Datas the import measure.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void ImportMeasure(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportRoleMeasure", "@tempDataImport");
        }

        /// <summary>
        /// Datas the import user.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void ImportUser(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportUserDataWithRole", "@UserDataImport");
        }

        /// <summary>
        /// Gets the user salary data.
        /// </summary>
        /// <returns>Data Table</returns>
        public DataTable GetUserSalaryData(int countryId)
        {            
            var applicationSetting = this.syngentaSIPUnitOfWork.ApplicationSettingRepository.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
            DataTable rst = null;
            rst = this.syngentaSIPContext.ExecuteProcedure("Proc_GetUserSalary",new SqlParameter("CountryId",countryId));
                   
            foreach (DataRow tempRow in rst.Rows)
            {
                tempRow.BeginEdit();

                if (tempRow["Salary"] != System.DBNull.Value)
                {
                    tempRow["Salary"] = Convert.ToDecimal(this.cryptoService.Decrypt(applicationSetting.Value, tempRow["Salary"].ToString()));
                }
                else
                {
                    tempRow["Salary"] = 0;
                }

                tempRow.EndEdit();
           }
            
            return rst;
        }

        /// <summary>
        /// Datas the import goal mount.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void DataImportGoalMount(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportGoalAmount", "@tempGoalAmount");
        }

        /// <summary>
        /// Gets the goal amount data.
        /// </summary>
        /// <returns>
        /// Data Table
        /// </returns>
        public DataTable GetGoalAmountData(int countryId)
        {
            DataTable rst = null;
            rst = this.syngentaSIPContext.ExecuteProcedure("Proc_GetGoalAmount", new SqlParameter("CountryId", countryId));
            foreach (DataRow tempRow in rst.Rows)
            {
                tempRow.BeginEdit();
                if (tempRow["Q1_1"] != System.DBNull.Value)
                {
                    tempRow["Q1_1"] = this.ConvertStringToDecimal(tempRow, "Q1_1"); 
                }
                else
                {
                    tempRow["Q1_1"] = 0;
                }

                if (tempRow["Q2_1"] != System.DBNull.Value)
                {
                    tempRow["Q2_1"] = this.ConvertStringToDecimal(tempRow, "Q2_1");
                }
                else
                {
                    tempRow["Q2_1"] = 0;
                }

                if (tempRow["Q3_1"] != System.DBNull.Value)
                {
                    tempRow["Q3_1"] = this.ConvertStringToDecimal(tempRow, "Q3_1");
                }
                else
                {
                    tempRow["Q3_1"] = 0;
                }

                if (tempRow["Q4_1"] != System.DBNull.Value)
                {
                    tempRow["Q4_1"] = this.ConvertStringToDecimal(tempRow, "Q4_1"); 
                }
                else
                {
                    tempRow["Q4_1"] = 0;
                }

                if (tempRow["Q1_2"] != System.DBNull.Value)
                {
                    tempRow["Q1_2"] = this.ConvertStringToDecimal(tempRow, "Q1_2");
                }
                else
                {
                    tempRow["Q1_2"] = 0;
                }

                if (tempRow["Q2_2"] != System.DBNull.Value)
                {
                    tempRow["Q2_2"] = this.ConvertStringToDecimal(tempRow, "Q2_2");
                }
                else
                {
                    tempRow["Q2_2"] = 0;
                }

                if (tempRow["Q3_2"] != System.DBNull.Value)
                {
                    tempRow["Q3_2"] = this.ConvertStringToDecimal(tempRow, "Q3_2");
                }
                else
                {
                    tempRow["Q3_2"] = 0;
                }

                if (tempRow["Q4_2"] != System.DBNull.Value)
                {
                    tempRow["Q4_2"] = this.ConvertStringToDecimal(tempRow, "Q4_2");
                }
                else
                {
                    tempRow["Q4_2"] = 0;
                }

                if (tempRow["Q1_3"] != System.DBNull.Value)
                {
                    tempRow["Q1_3"] = this.ConvertStringToDecimal(tempRow, "Q1_3");
                }
                else
                {
                    tempRow["Q1_3"] = 0;
                }   
                
                if (tempRow["Q2_3"] != System.DBNull.Value)
                {               
                    tempRow["Q2_3"] = this.ConvertStringToDecimal(tempRow, "Q2_3");
                }               
                else            
                {               
                    tempRow["Q2_3"] = 0;
                }    
                
                if (tempRow["Q3_3"] != System.DBNull.Value)
                {               
                    tempRow["Q3_3"] = this.ConvertStringToDecimal(tempRow, "Q3_3");
                }               
                else            
                {               
                    tempRow["Q3_3"] = 0;
                }      
                
                if (tempRow["Q4_3"] != System.DBNull.Value)
                {               
                    tempRow["Q4_3"] = this.ConvertStringToDecimal(tempRow, "Q4_3");
                }               
                else            
                {               
                    tempRow["Q4_3"] = 0;
                }
                
                if (tempRow["Q1_4"] != System.DBNull.Value)
                {
                    tempRow["Q1_4"] = this.ConvertStringToDecimal(tempRow, "Q1_4");
                }
                else
                {
                    tempRow["Q1_4"] = 0;
                }

                if (tempRow["Q2_4"] != System.DBNull.Value)
                {
                    tempRow["Q2_4"] = this.ConvertStringToDecimal(tempRow, "Q2_4");
                }
                else
                {
                    tempRow["Q2_4"] = 0;
                }

                if (tempRow["Q3_4"] != System.DBNull.Value)
                {
                    tempRow["Q3_4"] = this.ConvertStringToDecimal(tempRow, "Q3_4");
                }
                else
                {
                    tempRow["Q3_4"] = 0;
                }

                if (tempRow["Q4_4"] != System.DBNull.Value)
                {
                    tempRow["Q4_4"] = this.ConvertStringToDecimal(tempRow, "Q4_4");
                }
                else
                {
                    tempRow["Q4_4"] = 0;
                }

                if (tempRow["VisibilityDate"] != System.DBNull.Value)
                {
                    tempRow["VisibilityDate"] = tempRow["VisibilityDate"].ToString();
                }
                else
                {
                    tempRow["VisibilityDate"] = DateTime.UtcNow.Date;
                }

                tempRow.EndEdit();
            }
                        
            return rst;
        }

        /// <summary>
        /// Gets the payout history data.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>      
        public DataTable GetPayoutHistoryData(int countryId)
        {
            DataTable dataTable = null;
            SqlParameter year = new SqlParameter("@year", 2017);
            dataTable = this.syngentaSIPContext.ExecuteProcedure("Proc_GetPayoutHistory", new SqlParameter("CountryId", countryId));
            foreach (DataRow tempRow in dataTable.Rows)
            {
                tempRow.BeginEdit();
                if (tempRow["Year"] != DBNull.Value)
                {
                    tempRow["Year"] = Convert.ToInt32(tempRow["Year"]);
                }
                else
                {
                    tempRow["Year"] = 0;
                }

                if (tempRow["Employee ID"] != DBNull.Value)
                {
                    tempRow["Employee ID"] = Convert.ToString(tempRow["Employee ID"]);
                }
                else
                {
                    tempRow["Employee ID"] = 0;
                }

                if (tempRow["Plan Name"] != System.DBNull.Value)
                {
                    tempRow["Plan Name"] = Convert.ToString(tempRow["Plan Name"]);
                }
                else
                {
                    tempRow["Plan Name"] = "";
                }

                if (tempRow["User AD ID"] != System.DBNull.Value)
                {
                    tempRow["User AD ID"] = Convert.ToString(tempRow["User AD ID"]);
                }
                else
                {
                    tempRow["User AD ID"] = 0;
                }


                if (tempRow["Measure Name1"] != System.DBNull.Value)
                {
                    tempRow["Measure Name1"] = Convert.ToString(tempRow["Measure Name1"]);
                }
                else
                {
                    tempRow["Measure Name1"] = "";
                }

                if (tempRow["Freq1"] != System.DBNull.Value)
                {
                    tempRow["Freq1"] = Convert.ToString(tempRow["Freq1"]);
                }
                else
                {
                    tempRow["Freq1"] = 0;
                }

                if (tempRow["Q1_1"] != System.DBNull.Value)
                {
                    tempRow["Q1_1"] = this.ConvertStringToDecimal(tempRow, "Q1_1");
                }
                else
                {
                    tempRow["Q1_1"] = 0;
                }

                if (tempRow["Q2_1"] != System.DBNull.Value)
                {
                    tempRow["Q2_1"] = this.ConvertStringToDecimal(tempRow, "Q2_1");
                }
                else
                {
                    tempRow["Q2_1"] = 0;
                }

                if (tempRow["Q3_1"] != System.DBNull.Value)
                {
                    tempRow["Q3_1"] = this.ConvertStringToDecimal(tempRow, "Q3_1");
                }
                else
                {
                    tempRow["Q3_1"] = 0;
                }

                if (tempRow["Q4_1"] != System.DBNull.Value)
                {
                    tempRow["Q4_1"] = this.ConvertStringToDecimal(tempRow, "Q4_1");
                }
                else
                {
                    tempRow["Q4_1"] = 0;
                }
                //Measure2
                if (tempRow["Measure Name2"] != System.DBNull.Value)
                {
                    tempRow["Measure Name2"] = Convert.ToString(tempRow["Measure Name2"]);
                }
                else
                {
                    tempRow["Measure Name2"] = "";
                }

                if (tempRow["Freq2"] != System.DBNull.Value)
                {
                    tempRow["Freq2"] = Convert.ToString(tempRow["Freq2"]);
                }
                else
                {
                    tempRow["Freq2"] = 0;
                }

                if (tempRow["Q1_2"] != System.DBNull.Value)
                {
                    tempRow["Q1_2"] = this.ConvertStringToDecimal(tempRow, "Q1_2");
                }
                else
                {
                    tempRow["Q1_2"] = 0;
                }

                if (tempRow["Q2_2"] != System.DBNull.Value)
                {
                    tempRow["Q2_2"] = this.ConvertStringToDecimal(tempRow, "Q2_2");
                }
                else
                {
                    tempRow["Q2_2"] = 0;
                }

                if (tempRow["Q3_2"] != System.DBNull.Value)
                {
                    tempRow["Q3_2"] = this.ConvertStringToDecimal(tempRow, "Q3_2");
                }
                else
                {
                    tempRow["Q3_2"] = 0;
                }

                if (tempRow["Q4_2"] != System.DBNull.Value)
                {
                    tempRow["Q4_2"] = this.ConvertStringToDecimal(tempRow, "Q4_2");
                }
                else
                {
                    tempRow["Q4_2"] = 0;
                }
                //Measure3
                if (tempRow["Measure Name3"] != System.DBNull.Value)
                {
                    tempRow["Measure Name3"] = Convert.ToString(tempRow["Measure Name3"]);
                }
                else
                {
                    tempRow["Measure Name3"] = "";
                }

                if (tempRow["Freq3"] != System.DBNull.Value)
                {
                    tempRow["Freq3"] = Convert.ToString(tempRow["Freq3"]);
                }
                else
                {
                    tempRow["Freq3"] = 0;
                }

                if (tempRow["Q1_3"] != System.DBNull.Value)
                {
                    tempRow["Q1_3"] = this.ConvertStringToDecimal(tempRow, "Q1_3");
                }
                else
                {
                    tempRow["Q1_3"] = 0;
                }

                if (tempRow["Q2_3"] != System.DBNull.Value)
                {
                    tempRow["Q2_3"] = this.ConvertStringToDecimal(tempRow, "Q2_3");
                }
                else
                {
                    tempRow["Q2_3"] = 0;
                }

                if (tempRow["Q3_3"] != System.DBNull.Value)
                {
                    tempRow["Q3_3"] = this.ConvertStringToDecimal(tempRow, "Q3_3");
                }
                else
                {
                    tempRow["Q3_3"] = 0;
                }

                if (tempRow["Q4_3"] != System.DBNull.Value)
                {
                    tempRow["Q4_3"] = this.ConvertStringToDecimal(tempRow, "Q4_3");
                }
                else
                {
                    tempRow["Q4_3"] = 0;
                }
                //Mreasure4

                if (tempRow["Measure Name4"] != System.DBNull.Value)
                {
                    tempRow["Measure Name4"] = Convert.ToString(tempRow["Measure Name4"]);
                }
                else
                {
                    tempRow["Measure Name4"] = "";
                }

                if (tempRow["Freq4"] != System.DBNull.Value)
                {
                    tempRow["Freq4"] = Convert.ToString(tempRow["Freq4"]);
                }
                else
                {
                    tempRow["Freq4"] = 0;
                }
                //Mreasure4

                if (tempRow["Q1_4"] != System.DBNull.Value)
                {
                    tempRow["Q1_4"] = this.ConvertStringToDecimal(tempRow, "Q1_4");
                }
                else
                {
                    tempRow["Q1_4"] = 0;
                }

                if (tempRow["Q2_4"] != System.DBNull.Value)
                {
                    tempRow["Q2_4"] = this.ConvertStringToDecimal(tempRow, "Q2_4");
                }
                else
                {
                    tempRow["Q2_4"] = 0;
                }

                if (tempRow["Q3_4"] != System.DBNull.Value)
                {
                    tempRow["Q3_4"] = this.ConvertStringToDecimal(tempRow, "Q3_4");
                }
                else
                {
                    tempRow["Q3_4"] = 0;
                }

                if (tempRow["Q4_4"] != System.DBNull.Value)
                {
                    tempRow["Q4_4"] = this.ConvertStringToDecimal(tempRow, "Q4_4");
                }
                else
                {
                    tempRow["Q4_4"] = 0;
                }

                if (tempRow["VisibilityDate"] != System.DBNull.Value)
                {
                    tempRow["VisibilityDate"] = tempRow["VisibilityDate"].ToString();
                }
                else
                {
                    tempRow["VisibilityDate"] = DateTime.UtcNow.Date;
                }

                tempRow.EndEdit();
            }

            return dataTable;
        }        
        
        /// <summary>
        /// Datas the import payout history.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public void DataImportPayoutHistory(DataTable dataTable)
        {
            this.UploadDataTable(dataTable, "Proc_ImportUserPayoutHistory", "@DataImportPayoutHistory");
        }

        /// <summary>
        /// Uploads the data table.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="paramName">Name of the parameter.</param>
        private void UploadDataTable(DataTable dataTable, string procedureName, string paramName)
        {
            this.syngentaSIPContext.ExecuteProcedure(procedureName, new SqlParameter(paramName, dataTable));
        }

        /// <summary>
        /// Converts the string to decimal.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns> decimal value</returns>
        private decimal ConvertStringToDecimal(DataRow row, string columnName)
        {
            try
            {
                decimal result = 0;
                var applicationSetting = this.syngentaSIPUnitOfWork.ApplicationSettingRepository.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey);
                bool isParsed = decimal.TryParse(this.cryptoService.Decrypt(applicationSetting.Value, row[columnName].ToString()), out result);
                if (isParsed)
                {
                    return Math.Round(result,2);
                }
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        /// <summary>Gets the users by country identifier.</summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public DataTable GetUsersByCountryId(int countryId)
        {
            DataTable rst = null;
            rst = this.syngentaSIPContext.ExecuteProcedure("Proc_GetUsers", new SqlParameter("@CountryId", countryId));
            return rst;
        }
    }
}