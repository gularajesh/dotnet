// ***********************************************************************
// <copyright file="FrequencyModel.cs" company="Syngenta">
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
    /// FrequencyModel class
    /// </summary>
    public class FrequencyModel
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
        /// Gets or sets the frequency details.
        /// </summary>
        /// <value>
        /// The frequency details.
        /// </value>
        public virtual List<FrequencyDetailModel> FrequencyDetails { get; set; }
    }
}
