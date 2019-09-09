// ***********************************************************************
// <copyright file="PlanRepository.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;    

    /// <summary>
    /// PlanRepository class
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Repository.IPlanRepository" />
    public class PlanRepository : IPlanRepository
    {
        /// <summary>
        /// The syngenta sip context
        /// </summary>
        private readonly ISyngentaSIPContext syngentaSIPContext;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanRepository"/> class.
        /// </summary>
        /// <param name="syngentaSIPContext">The syngenta sip context.</param>
        public PlanRepository(ISyngentaSIPContext syngentaSIPContext)
        {
            this.syngentaSIPContext = syngentaSIPContext;
        }

        /// <summary>
        /// Gets the plan by role identifier user identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns plan by role id and user id
        /// </returns>
        public PlanModel GetPlanByRoleIdUserId(int roleId, int userId)
        {
            var plan = this.syngentaSIPContext.Plans.Where(x => x.RoleId == roleId && (x.VisibilityDate.Value <= DateTime.UtcNow)).OrderByDescending(x => x.Year).FirstOrDefault();
            //plan = plans.Where(x => x.VisibilityDate <= DateTime.UtcNow.Date).OrderByDescending(x => x.Year).FirstOrDefault();
            if (plan == null)
            {
                return plan;
            }

            return this.syngentaSIPContext.Plans
                .Include(x => x.PlanMeasures.Select(y => y.Frequency))
                .Include(x => x.PlanMeasures.Select(y => y.PayoutPercentage.PayoutPercentageDetails.Select(z => z.FrequencyDetail)))
                .Include(x => x.PlanMeasures.Select(y => y.PayoutCurve.PayoutCurveDetails))
                .Include(x => x.PlanModifiers.Select(y => y.PayoutCurve.PayoutCurveDetails))
                .Where(x => x.Id == plan.Id).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user payout history.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///  Returns PayoutHistory by User id
        /// </returns>
        public List<UserPayoutHistoryModel> GetUserPayoutHistory(int userId)
        {
            var payout = this.syngentaSIPContext.UserPayoutHistoryDetails.Where(x => x.UserId == userId);
            if (payout == null)
            {
                return null;
            }

            return this.syngentaSIPContext.UserPayoutHistoryDetails
                .Include(y => y.Frequency.FrequencyDetails.Select(z => z.Freqency))
                .Where(x => x.UserId == userId).OrderByDescending(x => x.Year).ToList();       
        }

        /// <summary>
        /// Gets the plan year.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>
        /// List of plan years
        /// </returns>
        public List<int> GetPlanYears(int roleId)
        {
            //var planYears = this.syngentaSIPContext.Plans.Where(x => x.RoleId == roleId && x.Year!=0).Select(x => x.Year).OrderByDescending(x=>x).ToList();            
            //if (planYears.Any())
            //{
            //    return planYears;
            //}
            //else
            //{
            //    return new CountryModel().Years.OrderByDescending(x=>x).ToList();
            //}            


            var planYears = new List<int>() { DateTime.UtcNow.Year - 1, DateTime.UtcNow.Year };
            return planYears.OrderByDescending(x=>x).ToList();
        }
    }
}
