// ***********************************************************************
// <copyright file="PlanMeasureModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// PlanMeasureModel class
    /// </summary>
    public class PlanMeasureModel
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
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets the MeasureSequence identifier.
        /// </summary>
        /// <value>
        /// The measure identifier.
        /// </value>  
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the measure identifier.
        /// </summary>
        /// <value>
        /// The measure identifier.
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
        /// Gets or sets the payout type identifier.
        /// </summary>
        /// <value>
        /// The payout type identifier.
        /// </value>        
        public int PayoutTypeId { get; set; }

        /// <summary>
        /// Gets or sets the payout percentage identifier.
        /// </summary>
        /// <value>
        /// The payout percentage identifier.
        /// </value>        
        public int PayoutPercentageId { get; set; }

        /// <summary>
        /// Gets or sets the measure weightage.
        /// </summary>
        /// <value>
        /// The measure weightage.
        /// </value>        
        public int PayoutCurveId { get; set; }

        /// <summary>
        /// Gets or sets the measure weightage.
        /// </summary>
        /// <value>
        /// The measure weightage.
        /// </value>
        public decimal MeasureWeightage { get; set; }

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

        ////[ForeignKey(nameof(RoleId))]
        ////public RoleModel Role { get; set; }        

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        [ForeignKey(nameof(FrequencyId))]
        public FrequencyModel Frequency { get; set; }

        /// <summary>
        /// Gets or sets the payout curve.
        /// </summary>
        /// <value>
        /// The payout Curve.
        /// </value>
        [ForeignKey(nameof(PayoutCurveId))]
        public PayoutCurveModel PayoutCurve { get; set; }

        /// <summary>
        /// Gets or sets the payout percentage.
        /// </summary>
        /// <value>
        /// The payout percentage.
        /// </value>
        [ForeignKey(nameof(PayoutPercentageId))]
        public PayoutPercentageModel PayoutPercentage { get; set; }

        /// <summary>
        /// Gets or sets the type of the payout.
        /// </summary>
        /// <value>
        /// The type of the payout.
        /// </value>
        [ForeignKey(nameof(PayoutTypeId))]
        public PayoutTypeModel PayoutType { get; set; }

        /// <summary>
        /// Gets or sets the plan.
        /// </summary>
        /// <value>
        /// The plan.
        /// </value>
        [ForeignKey(nameof(PlanId))]
        public PlanModel Plan { get; set; }        
    }
}
