// ***********************************************************************
// <copyright file="UserTargetDetailModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// class UserTargetDetailModel
    /// </summary>
    public class UserTargetDetailModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user target identifier.
        /// </summary>
        /// <value>
        /// The user target identifier.
        /// </value>
        public int UserTargetId { get; set; }

        /// <summary>
        /// Gets or sets the measure identifier.
        /// </summary>
        /// <value>
        /// The measure identifier.
        /// </value>
        public int PlanMeasureId { get; set; }

        /// <summary>
        /// Gets or sets the frequency detail identifier.
        /// </summary>
        /// <value>
        /// The frequency detail identifier.
        /// </value>
        public int FrequencyDetailId { get; set; }

        /// <summary>
        /// Gets or sets the incentive.
        /// </summary>
        /// <value>
        /// The incentive.
        /// </value>
        public string Incentive { get; set; }

        /// <summary>
        /// Gets or sets the goal.
        /// </summary>
        /// <value>
        /// The goal.
        /// </value>
        public string Goal { get; set; }

        /// <summary>
        /// Gets or sets the frequency detail.
        /// </summary>
        /// <value>
        /// The frequency detail.
        /// </value>
        [ForeignKey(nameof(FrequencyDetailId))]
        public FrequencyDetailModel FrequencyDetail { get; set; }

        /// <summary>
        /// Gets or sets the PlanMeasure.
        /// </summary>
        /// <value>
        /// The measure.
        /// </value>
        [ForeignKey(nameof(PlanMeasureId))]
        public PlanMeasureModel PlanMeasure { get; set; }

        /// <summary>
        /// Gets or sets the user target.
        /// </summary>
        /// <value>
        /// The user target.
        /// </value>
        [ForeignKey(nameof(UserTargetId))]
        public UserTargetModel UserTarget { get; set; }           
    }
}
