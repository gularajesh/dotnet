// ***********************************************************************
// <copyright file="UserSimulationModifierDetailModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// UserTargetSimulationModifierDetailModel class
    /// </summary>
    public class UserSimulationModifierDetailModel
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
        /// Gets or sets the user target simulation identifier.
        /// </summary>
        /// <value>
        /// The user target simulation identifier.
        /// </value>
        public int UserSimulationId { get; set; }

        /// <summary>
        /// Gets or sets the plan modifier identifier.
        /// </summary>
        /// <value>
        /// The plan modifier identifier.
        /// </value>
        public int PlanModifierId { get; set; }

        /// <summary>
        /// Gets or sets the modifier field value.
        /// </summary>
        /// <value>
        /// The modifier field value.
        /// </value>
        public int ModifierFieldValue { get; set; }

        /// <summary>
        /// Gets or sets the modifier payout.
        /// </summary>
        /// <value>
        /// The modifier payout.
        /// </value>
        public string ModifierPayout { get; set; }

        /// <summary>
        /// Gets or sets the user target simulation.
        /// </summary>
        /// <value>
        /// The user target simulation.
        /// </value>
        [ForeignKey(nameof(UserSimulationId))]
        public UserSimulationModel UserTargetSimulation { get; set; }

        /// <summary>
        /// Gets or sets the plan modifier.
        /// </summary>
        /// <value>
        /// The plan modifier.
        /// </value>
        [ForeignKey(nameof(PlanModifierId))]
        public PlanModifierModel PlanModifier { get; set; }
    }
}
