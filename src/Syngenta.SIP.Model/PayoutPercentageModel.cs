// ***********************************************************************
// <copyright file="PayoutPercentageModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// PayoutPercentageModel class
    /// </summary>
    public class PayoutPercentageModel
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the payout percentage details.
        /// </summary>
        /// <value>
        /// The payout percentage details.
        /// </value>
        public virtual List<PayoutPercentageDetailModel> PayoutPercentageDetails { get; set; }
    }
}
