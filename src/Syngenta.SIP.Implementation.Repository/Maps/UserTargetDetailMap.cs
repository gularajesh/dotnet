// ***********************************************************************
// <copyright file="UserTargetDetailMap.cs" company="Syngenta">
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
    /// UserTargetDetailMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserTargetDetailModel}" />
    public class UserTargetDetailMap : EntityTypeConfiguration<UserTargetDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserTargetDetailMap"/> class.
        /// </summary>
        public UserTargetDetailMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserTargetDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserTargetId).HasColumnName("UserTargetId");
            Property(x => x.PlanMeasureId).HasColumnName("PlanMeasureId");
            Property(x => x.FrequencyDetailId).HasColumnName("FrequencyDetailId");
            Property(x => x.Incentive).HasColumnName("Incentive");
            Property(x => x.Goal).HasColumnName("Goal");
        }
    }
}
