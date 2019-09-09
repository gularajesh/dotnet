// ***********************************************************************
// <copyright file="FrequencyDetialMap.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Repository.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Syngenta.SIP.Models;

    /// <summary>
    /// class FrequencyDetailMap
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.FrequencyDetailModel}" />
    public class FrequencyDetialMap : EntityTypeConfiguration<FrequencyDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyDetialMap"/> class.
        /// </summary>
        public FrequencyDetialMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("FrequencyDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.FrequencyId).HasColumnName("FrequencyId");
        }
    }
}
