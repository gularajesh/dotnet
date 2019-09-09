// ***********************************************************************
// <copyright file="PayoutPercentageDetailMap.cs" company="Syngenta">
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
    /// PayoutPercentageDetailMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PayoutPercentageDetailModel}" />
    public class PayoutPercentageDetailMap : EntityTypeConfiguration<PayoutPercentageDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutPercentageDetailMap"/> class.
        /// </summary>
        public PayoutPercentageDetailMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("PayoutPercentageDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.PayoutPercentageId).HasColumnName("PayoutPercentageId");
            Property(x => x.FrequencyDetailId).HasColumnName("FrequencyDetailId");
            Property(x => x.PayoutPercentageWeightage).HasColumnName("PayoutPercentage");
        }
    }
}
