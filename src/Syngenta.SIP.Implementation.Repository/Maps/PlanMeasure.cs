// ***********************************************************************
// <copyright file="PlanMeasure.cs" company="Syngenta">
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
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PlanMeasureModel}" />
    public class PlanMeasure : EntityTypeConfiguration<PlanMeasureModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanMeasure"/> class.
        /// </summary>
        public PlanMeasure()
        {
            this.HasKey(x => x.Id);

            this.ToTable("PlanMeasure");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.PlanId).HasColumnName("PlanId");
            Property(x => x.MeasureName).HasColumnName("MeasureName");
            Property(x => x.FrequencyId).HasColumnName("FrequencyId");
            Property(x => x.PayoutCurveId).HasColumnName("PayoutCurveId");
            Property(x => x.PayoutTypeId).HasColumnName("PayoutTypeId");
            Property(x => x.PayoutPercentageId).HasColumnName("PayoutPercentageId");
            Property(x => x.Sequence).HasColumnName("Sequence");
            Property(x => x.HasGoal).HasColumnName("HasGoal");
            Property(x => x.DataType).HasColumnName("DataType");
            Property(x => x.ValueType).HasColumnName("ValueType");
        }
    }
}
