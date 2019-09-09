// ***********************************************************************
// <copyright file="PlanViewModel.cs" company="Syngenta">
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
    /// class MeasureViewModel
    /// </summary>
    public class PlanViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanViewModel"/> class.
        /// </summary>
        public PlanViewModel()
        {
            this.RoleMeassures = new List<MeassureViewModel>();
            this.Modifiers = new List<ModifierViewModel>();
        }

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
        /// Gets or sets the simulation identifier.
        /// </summary>
        /// <value>
        /// The simulation identifier.
        /// </value>
        public int SimulationId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the business unit identifier.
        /// </summary>
        /// <value>
        /// The business unit identifier.
        /// </value>
        public int BussinessUnitId { get; set; }

        /// <summary>
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>
        /// The plan identifier.
        /// </value>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the total payout.
        /// </summary>
        /// <value>
        /// The total payout.
        /// </value>
        public decimal TotalPayout { get; set; }

        /// <summary>
        /// Gets or sets the total payout received.
        /// </summary>
        /// <value>
        /// The total payout received.
        /// </value>
        public decimal TotalPayoutReceived
        {
            get
            {
                if (this.RoleMeassures != null)
                {
                    return this.RoleMeassures.Sum(x => x.Incentive);
                }

                return 0;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the role measures.
        /// </summary>
        /// <value>
        /// The role measures.
        /// </value>
        public string SimulationName { get; set; }

        /// <summary>
        /// Gets or sets the role measures.
        /// </summary>
        /// <value>
        /// The role measures.
        /// </value>
        public List<MeassureViewModel> RoleMeassures { get; set; }

        /// <summary>
        /// Gets or sets the modifiers.
        /// </summary>
        /// <value>
        /// The modifiers.
        /// </value>
        public List<ModifierViewModel> Modifiers { get; set; }

        /// <summary>
        /// Updates the sequence.
        /// </summary>
        public void UpdateSequence()
        {
            if (this.RoleMeassures != null)
            {
                this.RoleMeassures = this.RoleMeassures.OrderBy(x => x.SequenceId).ThenBy(x => x.Id).ToList();
                this.RoleMeassures.ForEach(x => x.UpdateSequence());
            }
        }
    }
}
