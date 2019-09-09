// ***********************************************************************
// <copyright file="BasicDetailViewModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models.ViewModels
{
    using System;

    /// <summary>
    /// BasicDetailViewModel class
    /// </summary>
    public class BasicDetailViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the default language.
        /// </summary>
        /// <value>
        /// The default language.
        /// </value>
        public string DefaultLanguage { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the current role.
        /// </summary>
        /// <value>
        /// The current role.
        /// </value>
        public string CurrentRole { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the name of the bussiness unit.
        /// </summary>
        /// <value>
        /// The name of the bussiness unit.
        /// </value>
        public string BussinessUnitName { get; set; }

        /// <summary>
        /// Gets or sets the name of the plan.
        /// </summary>
        /// <value>
        /// The name of the plan.
        /// </value>
        public string PlanName { get; set; }

        /// <summary>
        /// Gets or sets the plan start date.
        /// </summary>
        /// <value>
        /// The plan start date.
        /// </value>
        public DateTime PlanStartDate { get; set; }

        /// <summary>
        /// Gets or sets the plan end date.
        /// </summary>
        /// <value>
        /// The plan end date.
        /// </value>
        public DateTime PlanEndDate { get; set; }

        /// <summary>
        /// Gets or sets the last logged in.
        /// </summary>
        /// <value>
        /// The last logged in.
        /// </value>
        public DateTime LastLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        /// <value>
        /// The language identifier.
        /// </value>
        public int LanguageID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
    }
}
