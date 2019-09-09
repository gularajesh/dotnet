// ***********************************************************************
// <copyright file="CountryMap.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Repository.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Syngenta.SIP.Models;

    /// <summary>
    /// class CountryMap
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.CountryModel}" />
    public class CountryMap : EntityTypeConfiguration<CountryModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryMap"/> class.
        /// </summary>
        public CountryMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("Country");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.TerritoryId).HasColumnName("TerritoryId");
            Property(x => x.LanguageId).HasColumnName("LanguageId");
            Property(x => x.CurrencyId).HasColumnName("CurrencyId");
        }
    }
}
