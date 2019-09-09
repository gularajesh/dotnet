// ***********************************************************************
// <copyright file="MeasureMap.cs" company="Syngenta">
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
    public class MeasureMap : EntityTypeConfiguration<MeasureModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasureMap"/> class.
        /// </summary>
        public MeasureMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("Measure");
            Property(x => x.Id).HasColumnName("Id");

            Property(x => x.MeasureName).HasColumnName("Name");
        }
    }
}
