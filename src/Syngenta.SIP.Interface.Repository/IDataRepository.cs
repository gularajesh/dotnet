// ***********************************************************************
// <copyright file="IDataRepository.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Interface.Repository
{
    using System.Data;
    using Syngenta.SIP.Models;

    /// <summary>
    /// IDataRepository class
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns data table
        /// </returns>
        DataTable GetCountries(int countryId);

        /// <summary>
        /// Gets the payout curves.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns data table
        /// </returns>
        DataTable GetPayoutCurves(int countryId);

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns data table
        /// </returns>
        DataTable GetRoles(int countryId);

        /// <summary>
        /// Gets the incentive payout.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns data table
        /// </returns>
        DataTable GetIncentivePayout(int countryId);

        /// <summary>
        /// Gets the measures.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// returns data table
        /// </returns>
        DataTable GetMeasures(int countryId);

        /// <summary>
        /// Gets the payout history data.
        /// </summary>
        /// <returns></returns>
        DataTable GetPayoutHistoryData(int countryId = 0);

        /// <summary>
        /// Uploads the salary.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void UploadSalary(DataTable dataTable);

        /// <summary>
        /// Datas the import country.
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
        /// Gets the user salary data.
        /// </summary>
        /// <returns>Data Table</returns>
        DataTable GetUserSalaryData(int countryId = 0);

        /// <summary>
        /// Datas the import goal mount.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void DataImportGoalMount(DataTable dataTable);

        /// <summary>
        /// Datas the import payout history.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        void DataImportPayoutHistory(DataTable dataTable);

        /// <summary>
        /// Gets the goal amount data.
        /// </summary>
        /// <returns>Data Table </returns>
        DataTable GetGoalAmountData(int countryId = 0);

        /// <summary>Gets the users.</summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        DataTable GetUsersByCountryId(int countryId = 0);
    }
}
