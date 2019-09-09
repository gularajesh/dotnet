// ***********************************************************************
// <copyright file="IPlanRepository.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Interface.Repository
{
    using System.Collections.Generic;
    using Syngenta.SIP.Models;

    /// <summary>
    /// IPlanRepository class
    /// </summary>
    public interface IPlanRepository
    {
        /// <summary>
        /// Gets the plan by role identifier user identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns plan by role id and user id</returns>
        PlanModel GetPlanByRoleIdUserId(int roleId, int userId);

        /// <summary>
        /// Gets the user payout history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        List<UserPayoutHistoryModel> GetUserPayoutHistory(int userId);

        /// <summary>
        /// Gets the plan year.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>
        /// List of plan years
        /// </returns>
        List<int> GetPlanYears(int roleId);
    }
}
