// ***********************************************************************
// <copyright file="UserSimulationMeasureFrequencyDetailModelMap.cs" company="Syngenta">
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
    /// UserTargetSimulationDetailMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserSimulationMeasureFrequencyDetailModel}" />
    public class UserSimulationMeasureFrequencyDetailModelMap : EntityTypeConfiguration<UserSimulationMeasureFrequencyDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSimulationMeasureFrequencyDetailModelMap"/> class.
        /// </summary>
        public UserSimulationMeasureFrequencyDetailModelMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("UserSimulationMeasureFrequencyDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserSimulationMeasureDetailId).HasColumnName("UserSimulationMeasureDetailId");
            Property(x => x.FrequencyDetailId).HasColumnName("FrequencyDetailId");
            Property(x => x.Achievement).HasColumnName("Achievement");
            Property(x => x.AchievementPercentage).HasColumnName("AchievementPercentage");
            Property(d => d.AdditionalFields).HasColumnType("xml");
            Property(x => x.PayoutAmount).HasColumnName("PayoutAmount");
            Property(x => x.CumulativePercentage).HasColumnName("CumulativePercentage");
            Property(x => x.IsValue).HasColumnName("IsValue");
        }
    }
}
