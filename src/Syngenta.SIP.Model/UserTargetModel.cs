// ***********************************************************************
// <copyright file="UserTargetModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// UserTargetsModel class
    /// </summary>
    public class UserTargetModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserTargetModel"/> class.
        /// </summary>
        public UserTargetModel()
        {
            this.UserSimulations = new List<UserSimulationModel>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the measure identifier.
        /// </summary>
        /// <value>
        /// The measure identifier.
        /// </value>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets the visibility date.
        /// </summary>
        /// <value>
        /// The visibility date.
        /// </value>
        public DateTime VisibilityDate { get; set; }

        /// <summary>
        /// Gets or sets the plan.
        /// </summary>
        /// <value>
        /// The plan.
        /// </value>
        [ForeignKey(nameof(PlanId))]
        public PlanModel Plan { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }

        /// <summary>
        /// Gets or sets the user target details.
        /// </summary>
        /// <value>
        /// The user target details.
        /// </value>
        public List<UserTargetDetailModel> UserTargetDetails { get; set; }

        /// <summary>
        /// Gets or sets the user target simulations.
        /// </summary>
        /// <value>
        /// The user target simulations.
        /// </value>
        public List<UserSimulationModel> UserSimulations { get; set; }
    }
}
