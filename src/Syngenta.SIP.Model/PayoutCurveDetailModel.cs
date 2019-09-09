// ***********************************************************************
// <copyright file="PayoutCurveDetailModel.cs" company="Syngenta">
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
    /// PayoutCurveDetailModel class
    /// </summary>
    public class PayoutCurveDetailModel
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
        /// Gets or sets the payout curve identifier.
        /// </summary>
        /// <value>
        /// The payout curve identifier.
        /// </value>
        public int PayoutCurveId { get; set; }

        /// <summary>
        /// Gets or sets the slab identifier.
        /// </summary>
        /// <value>
        /// The slab identifier.
        /// </value>
        public int SlabId { get; set; }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public decimal Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public decimal? MaxInDB { get; set; }

        /// <summary>
        /// Gets or sets the multiplier.
        /// </summary>
        /// <value>
        /// The multiplier.
        /// </value>
        public double Multiplier { get; set; }

        /// <summary>
        /// Gets or sets the payout curve.
        /// </summary>
        /// <value>
        /// The payout curve.
        /// </value>
        [ForeignKey(nameof(PayoutCurveId))]
        public PayoutCurveModel PayoutCurve { get; set; }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        [NotMapped]
        public decimal Max
        {
            get
            {
                return this.MaxInDB.HasValue ? this.MaxInDB.Value : 999;
            }

            set
            {
                this.MaxInDB = value;
            }
        }
    }
}
