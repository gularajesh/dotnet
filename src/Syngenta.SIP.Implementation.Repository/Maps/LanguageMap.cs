// ***********************************************************************
// <copyright file="LanguageMap.cs" company="Syngenta">
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
    /// LanguageMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.LanguageModel}" />
    public class LanguageMap : EntityTypeConfiguration<LanguageModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageMap"/> class.
        /// </summary>
        public LanguageMap()
        {
            this.HasKey(x => x.Id);
            this.ToTable("Language");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.Code).HasColumnName("Code");
        }
    }
}
