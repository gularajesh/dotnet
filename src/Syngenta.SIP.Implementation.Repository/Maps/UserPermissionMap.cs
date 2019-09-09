// ***********************************************************************
// <copyright file="UserPermissionMap.cs" company="Syngenta">
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
    /// User Permission Map
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserPermissionModel}" />
    internal class UserPermissionMap : EntityTypeConfiguration<UserPermissionModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPermissionMap"/> class.
        /// </summary>
        public UserPermissionMap()
        {
            // Primary Key
            this.HasKey(x => new { x.UserID, x.PermissionID });

            this.ToTable("UserPermission");
            Property(x => x.UserID).HasColumnName("UserID");
            Property(x => x.PermissionID).HasColumnName("PermissionID");
        }
    }
}
