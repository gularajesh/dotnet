// ***********************************************************************
// <copyright file="ApplicationSettingModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// class ApplicationSetting
    /// </summary>
    public class ApplicationSettingModel
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
        /// Gets or sets the name of the key.
        /// </summary>
        /// <value>
        /// The name of the key.
        /// </value>
        public string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}
