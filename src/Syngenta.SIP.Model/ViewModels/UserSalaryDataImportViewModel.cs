// ***********************************************************************
// <copyright file="UserSalaryDataImportViewModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// UserSalaryDataImportViewModel class
    /// </summary>
    public class UserSalaryDataImportViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the login identifier.
        /// </summary>
        /// <value>
        /// The login identifier.
        /// </value>
        public string LoginID { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        /// <value>
        /// The salary.
        /// </value>
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the salary in database.
        /// </summary>
        /// <value>
        /// The salary in database.
        /// </value>
        [NotMapped]
        public string SalaryInDB { get; set; }

        /// <summary>
        /// Gets or sets the LastLogin in database.
        /// </summary>
        /// <value>
        /// The LastLogin.
        /// </value>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the Country in database.
        /// </summary>
        /// <value>
        /// The Country.
        /// </value>
        public string Country { get; set; }

 
    }
}
