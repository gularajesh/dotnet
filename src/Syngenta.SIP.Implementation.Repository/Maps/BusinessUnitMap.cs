// ***********************************************************************
// <copyright file="BusinessUnitMap.cs" company="Syngenta">
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
    /// BusinessUnitMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.BusinessUnitModel}" />
    public class BusinessUnitMap : EntityTypeConfiguration<BusinessUnitModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessUnitMap"/> class.
        /// </summary>
        public BusinessUnitMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("BusinessUnit");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
