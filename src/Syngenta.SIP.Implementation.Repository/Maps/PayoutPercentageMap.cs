// ***********************************************************************
// <copyright file="PayoutPercentageMap.cs" company="Syngenta">
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
    /// PayoutPercentageMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PayoutPercentageModel}" />
    public class PayoutPercentageMap : EntityTypeConfiguration<PayoutPercentageModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutPercentageMap" /> class.
        /// </summary>
        public PayoutPercentageMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("PayoutPercentage");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
