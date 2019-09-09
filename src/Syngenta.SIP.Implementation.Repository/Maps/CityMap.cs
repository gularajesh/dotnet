// ***********************************************************************
// <copyright file="CityMap.cs" company="Syngenta">
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
    /// CityMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.CityModel}" />
    public class CityMap : EntityTypeConfiguration<CityModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CityMap"/> class.
        /// </summary>
        public CityMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("City");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
