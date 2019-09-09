// ***********************************************************************
// <copyright file="PlanModifierMap.cs" company="Syngenta">
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
    /// PlanMeasure class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PlanModifierModel}" />
    public class PlanModifierMap : EntityTypeConfiguration<PlanModifierModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanModifierMap"/> class.
        /// </summary>
        public PlanModifierMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("PlanModifier");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.PlanId).HasColumnName("PlanId");
            Property(x => x.ModifierName).HasColumnName("ModifierName");
            Property(x => x.PayoutCurveId).HasColumnName("PayoutCurveId");
            Property(x => x.DataType).HasColumnName("DataType");
            Property(x => x.ValueType).HasColumnName("ValueType");
        }
    }
}
