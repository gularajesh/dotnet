// ***********************************************************************
// <copyright file="TerritoryModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// TerritoryModel class
    /// </summary>
    public class TerritoryModel
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
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        [ForeignKey(nameof(RegionId))]
        public RegionModel Region { get; set; }
    }
}
