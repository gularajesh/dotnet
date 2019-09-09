// ***********************************************************************
// <copyright file="MeasureFrequencyDetailViewModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// class MeasureFrequencyDetailViewModel
    /// </summary>
    public class MeasureFrequencyDetailViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasureFrequencyDetailViewModel"/> class.
        /// </summary>
        public MeasureFrequencyDetailViewModel()
        {
            this.AdditionalFields = new Dictionary<string, int>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the target per.
        /// </summary>
        /// <value>
        /// The target per.
        /// </value>
        public decimal TargetPer { get; set; }

        /// <summary>
        /// Gets or sets the target amount.
        /// </summary>
        /// <value>
        /// The target amount.
        /// </value>
        public decimal Incentive { get; set; }

        /// <summary>
        /// Gets or sets the current incentive.
        /// </summary>
        /// <value>
        /// The current incentive.
        /// </value>
        public decimal CurrentIncentive { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Goal { get; set; }

        /// <summary>
        /// Gets or sets the actual achievement.
        /// </summary>
        /// <value>
        /// The actual achievement.
        /// </value>
        public decimal ActualAchievment { get; set; }

        /// <summary>
        /// Gets or sets the actual achievement percentage.
        /// </summary>
        /// <value>
        /// The actual achievement percentage.
        /// </value>
        public decimal ActualAchievmentPercentage { get; set; }

        /// <summary>
        /// Gets or sets the cumulative payout.
        /// </summary>
        /// <value>
        /// The cumulative payout.
        /// </value>
        public decimal YTDCumilativePayout { get; set; }

        /// <summary>
        /// Gets or sets the additional fields.
        /// </summary>
        /// <value>
        /// The additional fields.
        /// </value>
        public Dictionary<string, int> AdditionalFields
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public decimal CumulativePercentage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is measure type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is measure type; otherwise, <c>false</c>.
        /// </value>
        public bool? IsValue { get; set; }
    }
}
