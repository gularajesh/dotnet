// ***********************************************************************
// <copyright file="PlanMap.cs" company="Syngenta">
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
    /// PlanMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.PlanModel}" />
    public class PlanMap : EntityTypeConfiguration<PlanModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanMap"/> class.
        /// </summary>
        public PlanMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("Plan");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.RoleId).HasColumnName("RoleId");
            Property(x => x.TargetIncentivePercentage).HasColumnName("TargetIncentivePercentage");
            Property(x => x.Status).HasColumnName("Status");
            Property(x => x.Year).HasColumnName("Year");
            Property(x => x.StartDate).HasColumnName("StartDate");
            Property(x => x.EndDate).HasColumnName("EndDate");           
        }
    }
}
