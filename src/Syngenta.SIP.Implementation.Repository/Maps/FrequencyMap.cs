// ***********************************************************************
// <copyright file="FrequencyMap.cs" company="Syngenta">
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
    /// class FrequencyMap
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.FrequencyModel}" />
    public class FrequencyMap : EntityTypeConfiguration<FrequencyModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyMap"/> class.
        /// </summary>
        public FrequencyMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("Frequency");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            HasMany(a => a.FrequencyDetails).WithRequired(a => a.Freqency).HasForeignKey(a => a.FrequencyId);
        }
    }
}
