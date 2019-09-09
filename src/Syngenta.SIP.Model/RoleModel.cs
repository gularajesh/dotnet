// ***********************************************************************
// <copyright file="RoleModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// RoleModel class
    /// </summary>
    public class RoleModel
    {
        ////public RoleModel()
        ////{
        ////    this.RoleDetails = new List<RoleDetailModel>();
        ////}

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
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the business unit identifier.
        /// </summary>
        /// <value>
        /// The business unit identifier.
        /// </value>
        public int BusinessUnitId { get; set; }

        /// <summary>
        /// Gets or sets the business unit.
        /// </summary>
        /// <value>
        /// The business unit.
        /// </value>
        [ForeignKey(nameof(BusinessUnitId))]
        public BusinessUnitModel BusinessUnit { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [ForeignKey(nameof(CountryId))]
        public CountryModel Country { get; set; }        
    }
}
