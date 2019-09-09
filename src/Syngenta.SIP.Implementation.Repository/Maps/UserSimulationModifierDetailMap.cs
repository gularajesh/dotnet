// ***********************************************************************
// <copyright file="UserSimulationModifierDetailMap.cs" company="Syngenta">
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
    /// RegionMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserSimulationModifierDetailModel}" />
    public class UserSimulationModifierDetailMap : EntityTypeConfiguration<UserSimulationModifierDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSimulationModifierDetailMap"/> class.
        /// </summary>
        public UserSimulationModifierDetailMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserSimulationModifierDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserSimulationId).HasColumnName("UserSimulationId");
            Property(x => x.PlanModifierId).HasColumnName("PlanModifierId");
            Property(x => x.ModifierFieldValue).HasColumnName("ModifierFieldValue");
            Property(x => x.ModifierPayout).HasColumnName("ModifierPayout");        
        }
    }
}
