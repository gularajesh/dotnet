// ***********************************************************************
// <copyright file="PayoutTypeMap.cs" company="Syngenta">
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
    /// PayoutTypeMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PayoutTypeModel}" />
    public class PayoutTypeMap : EntityTypeConfiguration<PayoutTypeModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutTypeMap"/> class.
        /// </summary>
        public PayoutTypeMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("PayoutType");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
