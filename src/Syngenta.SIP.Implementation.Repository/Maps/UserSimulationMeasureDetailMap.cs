// ***********************************************************************
// <copyright file="UserSimulationMeasureDetailMap.cs" company="Syngenta">
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
    /// UserSimulationMeasureDetailMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserSimulationMeasureDetailModel}" />
    public class UserSimulationMeasureDetailMap : EntityTypeConfiguration<UserSimulationMeasureDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSimulationMeasureDetailMap" /> class.
        /// </summary>
        public UserSimulationMeasureDetailMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserSimulationMeasureDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserSimulationId).HasColumnName("UserSimulationId");
            Property(x => x.PlanMeasureId).HasColumnName("PlanMeasureId");
            Property(x => x.Achievement).HasColumnName("Achievement");
            Property(x => x.AchievementPercentage).HasColumnName("AchievementPercentage");
            Property(x => x.PayoutAmount).HasColumnName("PayoutAmount");
        }
    }
}
