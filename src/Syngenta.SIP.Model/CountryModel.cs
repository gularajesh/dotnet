// ***********************************************************************
// <copyright file="CountryModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using Syngenta.SIP.Models.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// CountryModel class
    /// </summary> 
    public class CountryModel
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the territory identifier.
        /// </summary>
        /// <value>
        /// The territory identifier.
        /// </value>
        public int TerritoryId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        /// <value>
        /// The language identifier.
        /// </value>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is salary editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is salary editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsSalaryEditable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is goal editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is goal editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsGoalEditable { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [ForeignKey(nameof(CurrencyId))]
        public CurrencyModel Currency { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [ForeignKey(nameof(LanguageId))]
        public LanguageModel Language { get; set; }

        /// <summary>
        /// Gets or sets the territory.
        /// </summary>
        /// <value>
        /// The territory.
        /// </value>
        [ForeignKey(nameof(TerritoryId))]
        public TerritoryModel Territory { get; set; }

        /// <summary>
        /// Gets or sets the year identifier.
        /// </summary>
        /// <value>
        /// The year identifier.
        /// </value>
        [NotMapped]
        public int Year { get; set; }

        /// <summary>
        /// Gets the years.
        /// </summary>
        /// <value>
        /// The years.
        /// </value>
        [NotMapped]
        public List<int> Years
        {
            get
            {
                var yearList = new List<int>();
                var currentYear = DateTime.Today.Year;

                for (int i = 2018 ; i <= currentYear + 1; i++)
                {
                    yearList.Add(i);
                }

                return yearList;
            }
        }
    }
}
