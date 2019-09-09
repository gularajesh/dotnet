// ***********************************************************************
// <copyright file="CurrencyMap.cs" company="Syngenta">
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
    /// CurrencyMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.CurrencyModel}" />
    public class CurrencyMap : EntityTypeConfiguration<CurrencyModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyMap"/> class.
        /// </summary>
        public CurrencyMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("Currency");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.Code).HasColumnName("Code");
        }
    }
}
