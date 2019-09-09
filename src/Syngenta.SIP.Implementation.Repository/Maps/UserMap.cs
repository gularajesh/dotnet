// ***********************************************************************
// <copyright file="UserMap.cs" company="Syngenta">
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
    /// UserMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserModel}" />
    public class UserMap : EntityTypeConfiguration<UserModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("User");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.LoginID).HasColumnName("LoginID");
            Property(x => x.Name).HasColumnName("UserName");
            Property(x => x.CountryId).HasColumnName("CountryId");
            Property(x => x.CityId).HasColumnName("CityId");
            Property(x => x.SaveSimulation).HasColumnName("SaveSimulation");
            Property(x => x.IsActive).HasColumnName("IsActive");
            Property(x => x.BusinessUnitId).HasColumnName("BusinessUnitId");
            Property(x => x.LanguageId).HasColumnName("LanguageId");
            Property(x => x.LastLogin).HasColumnName("LastLogin");
            Property(x => x.EmployeeId).HasColumnName("EmployeeID");
        }
    }
}
