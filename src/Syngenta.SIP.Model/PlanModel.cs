// ***********************************************************************
// <copyright file="PlanModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using ViewModels;

    /// <summary>
    /// PlanModel class
    /// </summary>
    public class PlanModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the target incentive percentage.
        /// </summary>
        /// <value>
        /// The target incentive percentage.
        /// </value>
        public decimal TargetIncentivePercentage { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the visibility date.
        /// </summary>
        /// <value>
        /// The visibility date.
        /// </value>
        public DateTime? VisibilityDate { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [ForeignKey(nameof(RoleId))]

        public RoleModel Role { get; set; }

        /// <summary>
        /// Gets or sets the plan measures.
        /// </summary>
        /// <value>
        /// The plan measures.
        /// </value>
        public List<PlanMeasureModel> PlanMeasures { get; set; }

        /// <summary>
        /// Gets or sets the plan modifiers.
        /// </summary>
        /// <value>
        /// The plan modifiers.
        /// </value>
        public List<PlanModifierModel> PlanModifiers { get; set; }
    }
}
