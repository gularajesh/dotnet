// ***********************************************************************
// <copyright file="UserSalaryDetailMap.cs" company="Syngenta">
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
    /// UserSalaryDetailMap class
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{Syngenta.SIP.Models.UserSalaryDetailModel}" />
    public class UserSalaryDetailMap : EntityTypeConfiguration<UserSalaryDetailModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSalaryDetailMap"/> class.
        /// </summary>
        public UserSalaryDetailMap()
        {
            this.HasKey(x => x.Id);

            this.ToTable("UserSalaryDetail");
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.StartDate).HasColumnName("StartDate");
            Property(x => x.EndDate).HasColumnName("EndDate");
            Property(x => x.SalaryInDb).HasColumnName("Salary");
        }
    }
}
