// ***********************************************************************
// <copyright file="UserSalaryDetailModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// UserSalaryDetailModel class
    /// </summary>
    public class UserSalaryDetailModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        /// <value>
        /// The salary.
        /// </value>
        [NotMapped]
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the salary in database.
        /// </summary>
        /// <value>
        /// The salary in database.
        /// </value>
        public string SalaryInDb { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdated time.
        /// </summary>
        /// <value>
        /// The LastUpdated time.
        /// </value>
        public DateTime? LastUpdated { get; set; }

        public DateTime? VisibilityDate { get; set; }

    }
}
