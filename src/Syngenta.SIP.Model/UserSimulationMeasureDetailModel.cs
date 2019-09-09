// ***********************************************************************
// <copyright file="UserSimulationMeasureDetailModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// class UserSimulationMeasureDetail
    /// </summary>
    public class UserSimulationMeasureDetailModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSimulationMeasureDetailModel"/> class.
        /// </summary>
        public UserSimulationMeasureDetailModel()
        {
            this.UserSimulationMeasureFrequencyDetails = new List<UserSimulationMeasureFrequencyDetailModel>();
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
        /// Gets or sets the simulation identifier.
        /// </summary>
        /// <value>
        /// The simulation identifier.
        /// </value>
        public int UserSimulationId { get; set; }

        /// <summary>
        /// Gets or sets the plan measure identifier.
        /// </summary>
        /// <value>
        /// The plan measure identifier.
        /// </value>
        public int PlanMeasureId { get; set; }

        /// <summary>
        /// Gets or sets the achievement.
        /// </summary>
        /// <value>
        /// The achievement.
        /// </value>
        public string Achievement { get; set; }

        /// <summary>
        /// Gets or sets the achievement percentage.
        /// </summary>
        /// <value>
        /// The achievement percentage.
        /// </value>
        public decimal AchievementPercentage { get; set; }

        /// <summary>
        /// Gets or sets the payout amount.
        /// </summary>
        /// <value>
        /// The payout amount.
        /// </value>
        public string PayoutAmount { get; set; }

        /// <summary>
        /// Gets or sets the measure.
        /// </summary>
        /// <value>
        /// The measure.
        /// </value>
        [ForeignKey(nameof(UserSimulationId))]
        public UserSimulationModel Measure { get; set; }

        /// <summary>
        /// Gets or sets the plan modifier.
        /// </summary>
        /// <value>
        /// The plan modifier.
        /// </value>
        [ForeignKey(nameof(PlanMeasureId))]
        public PlanMeasureModel PlanMeasure { get; set; }

        /// <summary>
        /// Gets or sets the user simulation measure frequency details.
        /// </summary>
        /// <value>
        /// The user simulation measure frequency details.
        /// </value>
        public List<UserSimulationMeasureFrequencyDetailModel> UserSimulationMeasureFrequencyDetails { get; set; }
    }
}
