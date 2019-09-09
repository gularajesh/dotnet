// ***********************************************************************
// <copyright file="IUserService.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Interface.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using Syngenta.SIP.Models;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// IUserService class
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>returns user by user name</returns>
        UserModel GetUserByUserName(string userName);

        /// <summary>
        /// Gets the countries list.
        /// </summary>
        /// <returns>returns list</returns>
        List<CountryModel> GetCountriesList();

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns>returns list of langauge</returns>
        List<LanguageModel> GetLanguageList();

        /// <summary>
        /// Saves the last logged in.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns user id</returns>
        int SaveLastLoggedIn(int userId);

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns user by user id</returns>
        UserModel GetUserById(int userId);

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns user details</returns>
        UserModel GetUserDetails(int userId);

        /// <summary>
        /// Gets the measures.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns plans</returns>
        PlanViewModel GetMeasures(UserModel user);

        /// <summary>
        /// Saves the salary details.
        /// </summary>
        /// <param name="salaryDetailViewModel">The salary detail view model.</param>
        /// <returns>returns true if saved else false</returns>
        bool SaveSalaryDetails(List<SalaryDetailViewModel> salaryDetailViewModel);

        /// <summary>
        /// Saves the measures details.
        /// </summary>
        /// <param name="measuresincomingdata">The measures incoming data.</param>
        /// <param name="saveSimulation">if set to <c>true</c> [save simulation].</param>
        /// <returns>returns string</returns>
        string SaveMeasuresDetails(PlanViewModel measuresincomingdata, bool saveSimulation);

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>returns all roles</returns>
        IQueryable<RoleModel> GetAllRoles();

        /// <summary>
        /// Gets the simulations.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns simulations</returns>
        List<PlanViewModel> GetSimulations(UserModel user);

        /// <summary>
        /// Deletes the simulation by identifier.
        /// </summary>
        /// <param name="simulationId">The simulation identifier.</param>
        /// <returns>returns string</returns>
        string DeleteSimulationById(int simulationId);

        /// <summary>
        /// Gets the payout history.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns model</returns>
        List<PlanViewModel> GetPayoutHistory(UserModel user);

        /// <summary>
        /// Saves the basic details.
        /// </summary>
        /// <param name="basicDetailViewModel">The basic detail view model.</param>
        /// <returns>returns true or false </returns>
        bool SaveBasicDetails(BasicDetailViewModel basicDetailViewModel);

        /// <summary>
        /// Gets the plan year.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>
        /// List of plan years
        /// </returns>
        List<int> GetPlanYears(int roleId);

        /// <summary>
        /// Gets the salary details.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        List<SalaryDetailViewModel> GetSalaryDetails(UserModel user);

        /// <summary>
        /// Gets the editable salary details.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Editable salary details </returns>
        SalaryDetailViewModel GetEditableSalaryDetails(UserModel user);
    }
}
