// ***********************************************************************
// <copyright file="UserSimulationModel.cs" company="Syngenta">
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
    /// class UserTargetSimulationModel
    /// </summary>
    public class UserSimulationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSimulationModel"/> class.
        /// </summary>
        public UserSimulationModel()
        {
            this.UserSimulationMeasureDetails = new List<UserSimulationMeasureDetailModel>();
            this.UserSimulationModifierDetails = new List<UserSimulationModifierDetailModel>();
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
        /// Gets or sets the target identifier.
        /// </summary>
        /// <value>
        /// The target identifier.
        /// </value>
        public int UserTargetId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the total payout.
        /// </summary>
        /// <value>
        /// The total payout.
        /// </value>
        public string TotalPayout { get; set; }

        /// <summary>
        /// Gets or sets the measure.
        /// </summary>
        /// <value>
        /// The measure.
        /// </value>
        [ForeignKey(nameof(UserTargetId))]
        public UserTargetModel Measure { get; set; }

        /// <summary>
        /// Gets or sets the user target simulation details.
        /// </summary>
        /// <value>
        /// The user target simulation details.
        /// </value>
        public List<UserSimulationMeasureDetailModel> UserSimulationMeasureDetails { get; set; }

        /// <summary>
        /// Gets or sets the modifier target simulation detail models.
        /// </summary>
        /// <value>
        /// The modifier target simulation detail models.
        /// </value>
        public List<UserSimulationModifierDetailModel> UserSimulationModifierDetails { get; set; }
    }
}
