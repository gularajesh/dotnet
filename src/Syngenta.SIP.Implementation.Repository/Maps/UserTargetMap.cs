// ***********************************************************************
// <copyright file="UserTargetMap.cs" company="Syngenta">
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
    /// UserTargetMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserTargetModel}" />
    public class UserTargetMap : EntityTypeConfiguration<UserTargetModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserTargetMap"/> class.
        /// </summary>
        public UserTargetMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserTarget");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.PlanId).HasColumnName("PlanId");
        }
    }
}
