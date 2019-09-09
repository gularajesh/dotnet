// ***********************************************************************
// <copyright file="ApplicationSettingMap.cs" company="Syngenta">
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
    /// class ApplicationSettingMap
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.ApplicationSettingModel}" />
    public class ApplicationSettingMap : EntityTypeConfiguration<ApplicationSettingModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingMap" /> class.
        /// </summary>
        public ApplicationSettingMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("ApplicationSetting");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.KeyName).HasColumnName("KeyName");
            Property(x => x.Value).HasColumnName("Value");
        }
    }
}
