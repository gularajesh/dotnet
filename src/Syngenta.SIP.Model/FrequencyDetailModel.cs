// ***********************************************************************
// <copyright file="FrequencyDetailModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary> 
    /// FrequencyDetailModel class
    /// </summary>
    public class FrequencyDetailModel
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
        /// Gets or sets the frequency identifier.
        /// </summary>
        /// <value>
        /// The frequency identifier.
        /// </value>
        public int FrequencyId { get; set; }

        /// <summary>
        /// Gets or sets the Frequency.
        /// </summary>
        /// <value>
        /// The Frequency.
        /// </value>
        [ForeignKey(nameof(FrequencyId))]
        public virtual FrequencyModel Freqency { get; set; }
    }
}
