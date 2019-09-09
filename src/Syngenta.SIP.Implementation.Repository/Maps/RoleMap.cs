// ***********************************************************************
// <copyright file="RoleMap.cs" company="Syngenta">
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
    /// RoleMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.RoleModel}" />
    public class RoleMap : EntityTypeConfiguration<RoleModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleMap"/> class.
        /// </summary>
        public RoleMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("Role");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.BusinessUnitId).HasColumnName("BusinessUnitId");
            Property(x => x.CountryId).HasColumnName("CountryId");
        }
    }
}
