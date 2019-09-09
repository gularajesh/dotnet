// ***********************************************************************
// <copyright file="PayoutPercentageDetailModel.cs" company="Syngenta">
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
    /// PayoutPercentageDetailModel class
    /// </summary>
    public class PayoutPercentageDetailModel
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
        /// Gets or sets the payout percentage identifier.
        /// </summary>
        /// <value>
        /// The payout percentage identifier.
        /// </value>
        public int PayoutPercentageId { get; set; }

        /// <summary>
        /// Gets or sets the frequency details identifier.
        /// </summary>
        /// <value>
        /// The frequency details identifier.
        /// </value>
        public int FrequencyDetailId { get; set; }

        /// <summary>
        /// Gets or sets the payout percentage weightage.
        /// </summary>
        /// <value>
        /// The payout percentage weightage.
        /// </value>
        public decimal PayoutPercentageWeightage { get; set; }

        /// <summary>
        /// Gets or sets the frequency detail.
        /// </summary>
        /// <value>
        /// The frequency detail.
        /// </value>
        [ForeignKey(nameof(FrequencyDetailId))]
        public FrequencyDetailModel FrequencyDetail { get; set; }

        /// <summary>
        /// Gets or sets the payout percentage.
        /// </summary>
        /// <value>
        /// The payout percentage.
        /// </value>
        [ForeignKey(nameof(PayoutPercentageId))]
        public PayoutPercentageModel PayoutPercentage { get; set; }
    }
}
