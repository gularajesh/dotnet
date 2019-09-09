// ***********************************************************************
// <copyright file="IUserRepository.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Interface.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Syngenta.SIP.Models;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// IUserRepository class
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>returns user by name</returns>
        UserModel GetUserByUserName(string userName);

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns user by id</returns>
        UserModel GetUserById(int userId);

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns user details</returns>
        UserModel GetUserDetails(int userId);

        /// <summary>
        /// Saves the salary details.
        /// </summary>
        /// <param name="userSalaryDetailList">The user salary detail list.</param>
        /// <returns>
        /// save salary details
        /// </returns>
        bool SaveSalaryDetails(List<UserSalaryDetailModel> userSalaryDetailList);

        /// <summary>
        /// Saves the last logged in.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>save last logged in</returns>
        int SaveLastLoggedIn(int userId);

        /// <summary>
        /// Gets the user by login identifier.
        /// </summary>
        /// <param name="loginId">The login identifier.</param>
        /// <returns>returns user model</returns>
        UserModel GetUserByLoginId(string loginId);

        /// <summary>
        /// Saves the measures details.
        /// </summary>
        /// <param name="measures">The measures.</param>
        /// <param name="saveSimulation">if set to <c>true</c> [save simulation].</param>
        void SaveMeasuresDetails(PlanViewModel measures, bool saveSimulation);

        /// <summary>
        /// Gets the user targets by user identifier plan identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="planId">The plan identifier.</param>
        /// <returns>get user targets by user id and plan id</returns>
        List<UserTargetModel> GetUserTargetsByUserIdPlanId(int userId, int planId);

        /// <summary>
        /// Gets the roles list.
        /// </summary>
        /// <returns>returns roles list</returns>
        List<RoleModel> GetRolesList();

        /// <summary>
        /// Gets the countries list.
        /// </summary>
        /// <returns>returns countries list</returns>
        List<CountryModel> GetCountriesList();

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns> returns Language list</returns>
        List<LanguageModel> GetLanguageList();

        /// <summary>
        /// Gets the business list.
        /// </summary>
        /// <returns>returns business unit list</returns>
        List<BusinessUnitModel> GetBussinessList();

        /// <summary>
        /// Changes the user role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="bussinessunitid">The business unit id.</param>
        /// <param name="countryid">The country id.</param>
        /// <param name="roleid">The role id.</param>
        void ChangeUserRole(UserModel user, int bussinessunitid, int countryid, int roleid);

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>returns all roles</returns>
        IQueryable<RoleModel> GetAllRoles();

        /// <summary>
        /// Deletes the simulation by identifier.
        /// </summary>
        /// <param name="simulationId">The simulation identifier.</param>
        void DeleteSimulationById(int simulationId);

        /// <summary>
        /// Saves the basic details.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns> returns true or false</returns>
        bool SaveBasicDetails(UserModel userModel);
    }
}