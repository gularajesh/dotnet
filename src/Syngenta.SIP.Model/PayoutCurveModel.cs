// ***********************************************************************
// <copyright file="PayoutCurveModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// PayoutCurveModel class
    /// </summary>
    public class PayoutCurveModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutCurveModel"/> class.
        /// </summary>
        public PayoutCurveModel()
        {
            this.PayoutCurveDetails = new List<PayoutCurveDetailModel>();
        }

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
        /// Gets or sets the payout curve type identifier.
        /// </summary>
        /// <value>
        /// The payout curve type identifier.
        /// </value>
        public int PayoutCurveTypeId { get; set; }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        [NotMapped]
        public decimal Min
        {
            get
            {
                if (this.PayoutCurveDetails != null)
                {
                    return this.PayoutCurveDetails.Select(x => x.Min).OrderBy(x => x).FirstOrDefault();
                }

                return 0;
            }

            set
            {
            }
        }

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
                if (this.PayoutCurveDetails != null)
                {
                    decimal maxvalue = this.PayoutCurveDetails.Where(x => x.Max == 999).Select(x => x.Min).FirstOrDefault();
                    if (maxvalue <= 0)
                    {
                        maxvalue = this.PayoutCurveDetails.Select(x => x.Max).OrderByDescending(x => x).FirstOrDefault();
                    }

                    decimal reminder = (maxvalue + 8) % 8;
                    if (reminder > 0)
                    {
                        return maxvalue + 8 - reminder;
                    }
                    else
                    {
                        return maxvalue + 8;
                    }
                }

                return 0;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the payout curve details.
        /// </summary>
        /// <value>
        /// The payout curve details.
        /// </value>
        public List<PayoutCurveDetailModel> PayoutCurveDetails { get; set; }

        /// <summary>
        /// Gets or sets the type of the payout curve.
        /// </summary>
        /// <value>
        /// The type of the payout curve.
        /// </value>
        [ForeignKey(nameof(PayoutCurveTypeId))]
        public PayoutCurveTypeModel PayoutCurveType { get; set; }
    }
}
