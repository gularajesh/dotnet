// ***********************************************************************
// <copyright file="PayoutCurveDetailMap.cs" company="Syngenta">
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
    /// PayoutCurveDetailMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PayoutCurveDetailModel}" />
    public class PayoutCurveDetailMap : EntityTypeConfiguration<PayoutCurveDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutCurveDetailMap"/> class.
        /// </summary>
        public PayoutCurveDetailMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("PayoutCurveDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.PayoutCurveId).HasColumnName("PayoutCurveId");
            Property(x => x.SlabId).HasColumnName("SlabId");
            Property(x => x.Min).HasColumnName("Min");
            Property(x => x.MaxInDB).HasColumnName("Max");
            Property(x => x.Multiplier).HasColumnName("Multiplier");
        }
    }
}
