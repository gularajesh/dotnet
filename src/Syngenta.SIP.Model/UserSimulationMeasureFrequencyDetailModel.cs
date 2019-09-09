// ***********************************************************************
// <copyright file="UserSimulationMeasureFrequencyDetailModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Linq;

    /// <summary>
    /// class UserTargetSimulationDetailModel
    /// </summary>
    public class UserSimulationMeasureFrequencyDetailModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
         public int Id { get; set; }

        /// <summary>
        /// Gets or sets the payout curve.
        /// </summary>
        /// <value>
        /// The payout curve.
        /// </value>
        public int UserSimulationMeasureDetailId { get; set; }

        /// <summary>
        /// Gets or sets the frequency detail identifier.
        /// </summary>
        /// <value>
        /// The frequency detail identifier.
        /// </value>
        public int FrequencyDetailId { get; set; }

        /// <summary>
        /// Gets or sets the cumulative percentage.
        /// </summary>
        /// <value>
        /// The cumulative percentage.
        /// </value>
        public decimal? CumulativePercentage { get; set; }
        
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
        /// Gets or sets the additional fields.
        /// </summary>
        /// <value>
        /// The additional fields.
        /// </value>
        public string AdditionalFields { get; set; }

        /// <summary>
        /// Gets or sets the payout amount.
        /// </summary>
        /// <value>
        /// The payout amount.
        /// </value>
        public string PayoutAmount { get; set; }

        /// <summary>
        /// Gets or sets the type of the is measure.
        /// </summary>
        /// <value>
        /// The type of the is measure.
        /// </value>
        public bool? IsValue { get; set; }

        /// <summary>
        /// Gets or sets the user target simulation.
        /// </summary>
        /// <value>
        /// The user target simulation.
        /// </value>
        [ForeignKey(nameof(UserSimulationMeasureDetailId))]
        public UserSimulationMeasureDetailModel UserTargetSimulation { get; set; }        
    }
}
