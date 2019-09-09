// ***********************************************************************
// <copyright file="UserModel.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// UserModel class
    /// </summary>
    public class UserModel
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
        /// Gets or sets the ad id.
        /// </summary>
        /// <value>
        /// The ad id.
        /// </value>
        public string LoginID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        /// <value>
        /// The Email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public string EmployeeId { get; set; }

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
        public int BusinessUnitId { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [save simulation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [save simulation]; otherwise, <c>false</c>.
        /// </value>
        public bool SaveSimulation { get; set; }

        /// <summary>
        /// Gets or sets the default language.
        /// </summary>
        /// <value>
        /// The default language.
        /// </value>
        public int? LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the last login.
        /// </summary>
        /// <value>
        /// The last login.
        /// </value>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the lm login identifier.
        /// </summary>
        /// <value>
        /// The lm login identifier.
        /// </value>
        public string LMLoginID { get; set; }

        /// <summary>
        /// Gets or sets the lmid.
        /// </summary>
        /// <value>
        /// The lmid.
        /// </value>
        public string LMID { get; set; }

        /// <summary>
        /// Gets or sets the name of the lm.
        /// </summary>
        /// <value>
        /// The name of the lm.
        /// </value>
        public string LMName { get; set; }

        /// <summary>
        /// Gets or sets the lm zone.
        /// </summary>
        /// <value>
        /// The lm zone.
        /// </value>
        public string Zone { get; set; }

        /// <summary>
        /// Gets or sets the business unit.
        /// </summary>
        /// <value>
        /// The business unit.
        /// </value>
        [ForeignKey(nameof(BusinessUnitId))]
        public BusinessUnitModel BusinessUnit { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [ForeignKey(nameof(CityId))]
        public CityModel City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [ForeignKey(nameof(CountryId))]
        public CountryModel Country { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [ForeignKey(nameof(LanguageId))]
        public LanguageModel Language { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        /// <value>
        /// The user details.
        /// </value>
        public List<UserRoleDetailModel> RoleDetails { get; set; }

        /// <summary>
        /// Gets or sets the salary details.
        /// </summary>
        /// <value>
        /// The salary details.
        /// </value>
        public List<UserSalaryDetailModel> SalaryDetails { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public List<UserPermissionModel> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the current plan.
        /// </summary>
        /// <value>
        /// The current plan.
        /// </value>
        [NotMapped]
        public PlanModel CurentPlan { get; set; }

        /// <summary>
        /// Gets the current role.
        /// </summary>
        /// <value>
        /// The current role.
        /// </value>
        [NotMapped]
        public UserRoleDetailModel CurentRole
        {
            get
            {
                if (this.RoleDetails == null || this.RoleDetails.Count <= 0)
                {
                    return null;
                }

                return this.RoleDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the current salary.
        /// </summary>
        /// <value>
        /// The current salary.
        /// </value>
        [NotMapped]
        public UserSalaryDetailModel CurentSalary
        {
            get
            {
                if (this.SalaryDetails == null || this.SalaryDetails.Count <= 0)
                {
                    return new UserSalaryDetailModel() { StartDate = new DateTime(DateTime.Today.Year, 1, 1) };
                }

                return this.SalaryDetails.OrderByDescending(x => x.Id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the salaries by quarter.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// returns salaries by quarter
        /// </returns>
        public Dictionary<FrequencyDetails, decimal> GetSalariesByQuarter(int year, bool isSalaryDetailsExists = true)
        {
            var result = new Dictionary<FrequencyDetails, decimal>();
            result[FrequencyDetails.Annual] = 0;
            result[FrequencyDetails.H1] = 0;
            result[FrequencyDetails.H2] = 0;
            result[FrequencyDetails.Q1] = 0;
            result[FrequencyDetails.Q2] = 0;
            result[FrequencyDetails.Q3] = 0;
            result[FrequencyDetails.Q4] = 0;

            if (this.SalaryDetails == null || this.SalaryDetails.Count <= 0 || isSalaryDetailsExists == false)
            {
                return result;
            }

            var q1_StartDate = new DateTime(year, 1, 1);
            var q1_EndDate = new DateTime(year, 3, 31);
            var q2_EndDate = new DateTime(year, 6, 30);
            var q3_EndDate = new DateTime(year, 9, 30);
            var q4_EndDate = new DateTime(year, 12, 31);

            var resultItem = this.SalaryDetails.Where(x => (x.StartDate <= q3_EndDate.AddDays(1) && x.EndDate >= q4_EndDate) || (x.StartDate <= q3_EndDate.AddDays(1) && x.EndDate.HasValue == false)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (resultItem != null)
            {
                result[FrequencyDetails.Annual] = Convert.ToDecimal(resultItem.Salary);
            }

            resultItem = this.SalaryDetails.Where(x => (x.StartDate <= q1_StartDate.AddDays(1) && x.EndDate >= q2_EndDate) || (x.StartDate <= q1_EndDate.AddDays(1) && x.EndDate.HasValue == false)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (resultItem != null)
            {
                result[FrequencyDetails.H1] = resultItem.Salary;
            }

            resultItem = this.SalaryDetails.Where(x => (x.StartDate <= q3_EndDate.AddDays(1) && x.EndDate >= q4_EndDate) || (x.StartDate <= q3_EndDate.AddDays(1) && x.EndDate.HasValue == false)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (resultItem != null)
            {
                result[FrequencyDetails.H2] = resultItem.Salary;
            }

            resultItem = this.SalaryDetails.Where(x => (x.StartDate <= q1_StartDate && x.EndDate >= q1_EndDate) || (x.StartDate <= q1_StartDate && x.EndDate.HasValue == false)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (resultItem != null)
            {
                result[FrequencyDetails.Q1] = resultItem.Salary;
            }

            resultItem = this.SalaryDetails.Where(x => (x.StartDate <= q1_EndDate.AddDays(1) && x.EndDate >= q2_EndDate) || (x.StartDate <= q1_EndDate.AddDays(1) && x.EndDate.HasValue == false)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (resultItem != null)
            {
                result[FrequencyDetails.Q2] = resultItem.Salary;
            }

            resultItem = this.SalaryDetails.Where(x => (x.StartDate <= q2_EndDate.AddDays(1) && x.EndDate >= q3_EndDate) || (x.StartDate <= q2_EndDate.AddDays(1) && x.EndDate.HasValue == false)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (resultItem != null)
            {
                result[FrequencyDetails.Q3] = resultItem.Salary;
            }

            resultItem = this.SalaryDetails.Where(x => (x.StartDate <= q3_EndDate.AddDays(1) && x.EndDate >= q4_EndDate) || (x.StartDate <= q3_EndDate.AddDays(1) && x.EndDate.HasValue == false)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (resultItem != null)
            {
                result[FrequencyDetails.Q4] = resultItem.Salary;
            }

            return result;
        }
    }
}