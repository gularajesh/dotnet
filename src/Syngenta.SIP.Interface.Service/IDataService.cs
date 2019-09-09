// ***********************************************************************
// <copyright file="IDataService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Interface.Service
{
    using System.Collections.Generic;
    using System.Data;
    using Syngenta.SIP.Models;

    /// <summary>
    /// IDataService class
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Generates the data file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="id">The identifier.</param>
        void GenerateDataFile(System.IO.MemoryStream path, int id);

        /// <summary>
        /// Generates the user data file.
        /// </summary>
        /// <param name="path">The path.</param>
        void GenerateUserDataFile(System.IO.MemoryStream path,int id = 0);

        /// <summary>
        /// Salaries the import.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>returns true or false</returns>
        bool SalaryImport(DataTable dt);

        /// <summary>
        /// Datas the import.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void ImportCountry(DataTable dataTable);

        /// <summary>
        /// Datas the import incentive payout.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void ImportIncentivePayout(DataTable dataTable);

        /// <summary>
        /// Datas the import payout curve.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void ImportPayoutCurve(DataTable dataTable);

        /// <summary>
        /// Datas the import role.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void ImportRole(DataTable dataTable);

        /// <summary>
        /// Datas the import measure.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void ImportMeasure(DataTable dataTable);

        /// <summary>
        /// Datas the import user.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void ImportUser(DataTable dataTable);

        /// <summary>
        /// Generates the salary data file.
        /// </summary>
        /// <param name="path">The path.</param>
        void GenerateSalaryDataFile(System.IO.MemoryStream path);

        /// <summary>
        /// Imports the goal.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void ImportGoal(DataTable dataTable);

        /// <summary>
        /// Imports the payout history.
        /// </summary>
        /// <param name="excelTable">The excel table.</param>
        void ImportPayoutHistory(DataTable excelTable);

        /// <summary>
        /// Generates the goal data file.
        /// </summary>
        /// <param name="path">The path.</param>
        void GenerateGoalDataFile(System.IO.MemoryStream path);
    }
}
