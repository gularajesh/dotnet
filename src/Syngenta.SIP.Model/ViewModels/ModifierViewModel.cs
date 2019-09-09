// ***********************************************************************
// <copyright file="ModifierViewModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models.ViewModels
{
    /// <summary>
    /// ModifierViewModel class
    /// </summary>
    public class ModifierViewModel
    {
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
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the payout curve.
        /// </summary>
        /// <value>
        /// The payout curve.
        /// </value>
        public PayoutCurveModel PayoutCurve
        {
            get; set;
        }

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
        public decimal ModifierPayout { get; set; }

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
    }
}