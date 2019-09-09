// ***********************************************************************
// <copyright file="PermissionMap.cs" company="Syngenta">
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
    /// Permission Map
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PermissionModel}" />
    internal class PermissionMap : EntityTypeConfiguration<PermissionModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionMap"/> class.
        /// </summary>
        public PermissionMap()
        {
            this.HasKey(x => x.ID);

            this.ToTable("Permission");
            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
