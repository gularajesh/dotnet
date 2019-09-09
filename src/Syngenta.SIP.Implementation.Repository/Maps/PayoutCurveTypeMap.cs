// ***********************************************************************
// <copyright file="PayoutCurveTypeMap.cs" company="Syngenta">
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
    public class PayoutCurveTypeMap : EntityTypeConfiguration<PayoutCurveTypeModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutCurveTypeMap"/> class.
        /// </summary>
        public PayoutCurveTypeMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("PayoutCurveType");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
