// ***********************************************************************
// <copyright file="PlanModifierModel.cs" company="Syngenta">
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
    /// PlanModifierModel class
    /// </summary>
    public class PlanModifierModel
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
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>
        /// The plan identifier.
        /// </value>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets the name of the modifier.
        /// </summary>
        /// <value>
        /// The name of the modifier.
        /// </value>
        public string ModifierName { get; set; }

        /// <summary>
        /// Gets or sets the payout curve identifier.
        /// </summary>
        /// <value>
        /// The payout curve identifier.
        /// </value>
        public int PayoutCurveId { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public string ValueType { get; set; }

        /// <summary>
        /// Gets or sets the plan.
        /// </summary>
        /// <value>
        /// The plan.
        /// </value>
        [ForeignKey(nameof(PlanId))]
        public PlanModel Plan { get; set; }

        /// <summary>
        /// Gets or sets the PayoutCurve.
        /// </summary>
        /// <value>
        /// The modifier.
        /// </value>
        [ForeignKey(nameof(PayoutCurveId))]
        public PayoutCurveModel PayoutCurve { get; set; }
    }
}
