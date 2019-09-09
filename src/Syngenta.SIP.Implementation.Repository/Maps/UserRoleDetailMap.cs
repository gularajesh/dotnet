// ***********************************************************************
// <copyright file="UserRoleDetailMap.cs" company="Syngenta">
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
    /// UserRoleDetailMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserRoleDetailModel}" />
    public class UserRoleDetailMap : EntityTypeConfiguration<UserRoleDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleDetailMap" /> class.
        /// </summary>
        public UserRoleDetailMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserRoleDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.RoleId).HasColumnName("RoleId");
            Property(x => x.StartDate).HasColumnName("StartDate");
            Property(x => x.EndDate).HasColumnName("EndDate");
        }
    }
}
