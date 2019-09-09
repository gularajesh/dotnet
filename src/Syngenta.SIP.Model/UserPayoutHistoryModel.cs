// ***********************************************************************
// <copyright file="UserPayoutHistoryModel.cs" company="Syngenta">
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
    /// UserPayoutHistoryModel class
    /// </summary>
    public class UserPayoutHistoryModel
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
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the name of the plan.
        /// </summary>
        /// <value>
        /// The name of the plan.
        /// </value>        
        public string PlanName { get; set; }

        /// <summary>
        /// Gets or sets the name of the measure.
        /// </summary>
        /// <value>
        /// The name of the measure.
        /// </value>
        public string MeasureName { get; set; }

        /// <summary>
        /// Gets or sets the frequency identifier.
        /// </summary>
        /// <value>
        /// The frequency identifier.
        /// </value>
        public int FrequencyId { get; set; }

        /// <summary>
        /// Gets or sets the quarter1.
        /// </summary>
        /// <value>
        /// The quarter1.
        /// </value>
        public string Quarter1 { get; set; }

        /// <summary>
        /// Gets or sets the quarter2.
        /// </summary>
        /// <value>
        /// The quarter2.
        /// </value>
        public string Quarter2 { get; set; }

        /// <summary>
        /// Gets or sets the quarter3.
        /// </summary>
        /// <value>
        /// The quarter3.
        /// </value>
        public string Quarter3 { get; set; }

        /// <summary>
        /// Gets or sets the quarter4.
        /// </summary>
        /// <value>
        /// The quarter4.
        /// </value>
        public string Quarter4 { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        [ForeignKey(nameof(FrequencyId))]
        public FrequencyModel Frequency { get; set; }

        /// <summary>
        /// Gets or sets the visibility date.
        /// </summary>
        /// <value>
        /// The visibility date.
        /// </value>        
        public DateTime VisibilityDate { get; set; }
    }
}
