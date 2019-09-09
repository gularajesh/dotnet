// ***********************************************************************
// <copyright file="SalaryDetailViewModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models.ViewModels
{
    using System;

    /// <summary>
    /// SalaryDetailViewModel class
    /// </summary>
    public class SalaryDetailViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
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
        /// Gets or sets the base salary.
        /// </summary>
        /// <value>
        /// The base salary.
        /// </value>
        public decimal BaseSalary { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the target incentive per.
        /// </summary>
        /// <value>
        /// The target incentive per.
        /// </value>
        public decimal IncentivePercentage { get; set; }

        /// <summary>
        /// Gets or sets the target incentive.
        /// </summary>
        /// <value>
        /// The target incentive.
        /// </value>
        public decimal Incentive { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the business identifier.
        /// </summary>
        /// <value>
        /// The business identifier.
        /// </value>
        public int BusinessId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public int CountryId { get; set; }
    }
}
