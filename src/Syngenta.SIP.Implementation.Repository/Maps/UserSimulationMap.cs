// ***********************************************************************
// <copyright file="UserSimulationMap.cs" company="Syngenta">
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
    /// UserTargetSimulationMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserSimulationModel}" />
    public class UserSimulationMap : EntityTypeConfiguration<UserSimulationModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSimulationMap"/> class.
        /// </summary>
        public UserSimulationMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserSimulation");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserTargetId).HasColumnName("UserTargetId");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.TotalPayout).HasColumnName("TotalPayout");
        }
    }
}
