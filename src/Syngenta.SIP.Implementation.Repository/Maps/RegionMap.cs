// ***********************************************************************
// <copyright file="RegionMap.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Syngenta.SIP.Models;

    /// <summary>
    /// RegionMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.RegionModel}" />
    public class RegionMap : EntityTypeConfiguration<RegionModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionMap"/> class.
        /// </summary>
        public RegionMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("Region");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
