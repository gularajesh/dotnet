// ***********************************************************************
// <copyright file="ModifierModel.cs" company="Syngenta">
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
    /// ModifierModel class
    /// </summary>
    public class ModifierModel
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
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the payout curve identifier.
        /// </summary>
        /// <value>
        /// The payout curve identifier.
        /// </value>
        public int PayoutCurveId { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [ForeignKey(nameof(CityId))]
        public CityModel City { get; set; }

        /// <summary>
        /// Gets or sets the payout curve.
        /// </summary>
        /// <value>
        /// The payout curve.
        /// </value>
        [ForeignKey(nameof(PayoutCurveId))]
        public PayoutCurveModel PayoutCurve { get; set; }
    }
}
