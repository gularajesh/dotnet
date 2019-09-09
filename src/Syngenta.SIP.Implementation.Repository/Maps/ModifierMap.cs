// ***********************************************************************
// <copyright file="ModifierMap.cs" company="Syngenta">
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
    /// MeasureMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.MeasureModel}" />
    public class ModifierMap : EntityTypeConfiguration<ModifierModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierMap"/> class.
        /// </summary>
        public ModifierMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("Modifier");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.CityId).HasColumnName("CityId");
            Property(x => x.PayoutCurveId).HasColumnName("PayoutCurveId");
        }
    }
}
