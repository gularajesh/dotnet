// ***********************************************************************
// <copyright file="PayoutCurveMap.cs" company="Syngenta">
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
    /// PayoutCurveMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PayoutCurveModel}" />
    public class PayoutCurveMap : EntityTypeConfiguration<PayoutCurveModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutCurveMap"/> class.
        /// </summary>
        public PayoutCurveMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("PayoutCurve");

            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
