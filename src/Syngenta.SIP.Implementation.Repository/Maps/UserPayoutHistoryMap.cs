// ***********************************************************************
// <copyright file="UserPayoutHistoryMap.cs" company="Syngenta">
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
    /// UserPayoutHistoryMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserPayoutHistoryModel}" />
    public class UserPayoutHistoryMap : EntityTypeConfiguration<UserPayoutHistoryModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPayoutHistoryMap"/> class.
        /// </summary>
        public UserPayoutHistoryMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserPayoutHistory");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.Year).HasColumnName("Year");
            Property(x => x.PlanName).HasColumnName("PlanName");
            Property(x => x.MeasureName).HasColumnName("MeasureName");
            Property(x => x.FrequencyId).HasColumnName("FrequencyId");
            Property(x => x.Quarter1).HasColumnName("Quarter1");
            Property(x => x.Quarter2).HasColumnName("Quarter2");
            Property(x => x.Quarter3).HasColumnName("Quarter3");
            Property(x => x.Quarter4).HasColumnName("Quarter4");         
        }
    }
}
