// ***********************************************************************
// <copyright file="MeassureViewModel.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// RoleMeasures class
    /// </summary>
    public class MeassureViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeassureViewModel"/> class.
        /// </summary>
        public MeassureViewModel()
        {
            this.Fields = new List<MeasureFrequencyDetailViewModel>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the payout curve identifier.
        /// </summary>
        /// <value>
        /// The payout curve identifier.
        /// </value>
        public int PayoutCurveId { get; set; }

        /// <summary>
        /// Gets or sets the payout type identifier.
        /// </summary>
        /// <value>
        /// The payout type identifier.
        /// </value>
        public int PayoutTypeId { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        public string Frequency { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sequence identifier.
        /// </summary>
        /// <value>
        /// The sequence identifier.
        /// </value>
        public int SequenceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has goal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has goal; otherwise, <c>false</c>.
        /// </value>
        public bool HasGoal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has kpi.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has kpi; otherwise, <c>false</c>.
        /// </value>
        public bool IsKPI { get; set; }

        /// <summary>
        /// Gets or sets the target incentive per.
        /// </summary>
        /// <value>
        /// The target incentive per.
        /// </value>
        public string MeasureTargetPercentage { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public string ValueType { get; set; }

        /// <summary>
        /// Gets or sets the target incentive
        /// </summary>
        /// <value>
        /// The target incentive.
        /// </value>
        public decimal Incentive
        {
            get
            {
                if (this.Fields != null)
                {
                    return this.Fields.Sum(x => x.Incentive);
                }

                return 0;
            }

            set
            {
            }
        }

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
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public decimal Goal
        {
            get
            {
                if (this.Fields != null)
                {
                    return this.Fields.Sum(x => x.Goal);
                }

                return 0;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public List<MeasureFrequencyDetailViewModel> Fields { get; set; }

        /// <summary>
        /// Gets or sets the payout curve.
        /// </summary>
        /// <value>
        /// The payout curve.
        /// </value>
        public PayoutCurveModel PayoutCurve { get; set; }

        /// <summary>
        /// Updates the sequence.
        /// </summary>
        public void UpdateSequence()
        {
            if (this.PayoutCurve != null && this.PayoutCurve.PayoutCurveDetails != null)
            {
                this.PayoutCurve.PayoutCurveDetails = this.PayoutCurve.PayoutCurveDetails.OrderBy(x => x.SlabId).ToList();
            }

            if (this.Fields != null)
            {
                this.Fields = this.Fields.OrderBy(x => x.Id).ToList();
            }
        }
    }
}
