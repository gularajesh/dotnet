// ***********************************************************************
// <copyright file="TerritoryMap.cs" company="Syngenta">
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
    /// TerritoryMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.TerritoryModel}" />
    public class TerritoryMap : EntityTypeConfiguration<TerritoryModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryMap"/> class.
        /// </summary>
        public TerritoryMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("Territory");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.RegionId).HasColumnName("RegionId");
        }
    }
}
