// ***********************************************************************
// <copyright file="EnumUtility.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Roles Enumeration
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// The global lead
        /// </summary>
        Admin = 1,
    }

    /// <summary>
    /// Roles Enumeration
    /// </summary>
    public enum Permissions
    {
        /// <summary>
        /// The global lead
        /// </summary>
        EditUser = 1,

        /// <summary>
        /// The read user
        /// </summary>
        ReadUser = 2,

        /// <summary>
        /// The edit plan
        /// </summary>
        EditPlan = 3,

        /// <summary>
        /// The read plan
        /// </summary>
        ReadPlan = 4,

        /// <summary>
        /// The edit goal
        /// </summary>
        EditGoal = 5,

        /// <summary>
        /// The read goal
        /// </summary>
        ReadGoal = 6,

        /// <summary>
        /// The edit salary
        /// </summary>
        EditSalary = 7,

        /// <summary>
        /// The read salary
        /// </summary>
        ReadSalary = 8,

        /// <summary>
        /// The generate key
        /// </summary>
        GenerateKey = 9,

        /// <summary>
        /// The upload document
        /// </summary>
        UploadDocument = 10,

        /// <summary>
        /// The edit payout history
        /// </summary>
        EditPayoutHistory = 12,

        /// <summary>
        /// The read payout history
        /// </summary>
        ReadPayoutHistory = 11
    }

    /// <summary>
    /// FrequencyDetails Enumeration
    /// </summary>
    public enum FrequencyDetails
    {
        /// <summary>
        /// The annual
        /// </summary>
        Annual = 1,

        /// <summary>
        /// The h1
        /// </summary>
        H1 = 2,

        /// <summary>
        /// The h2
        /// </summary>
        H2 = 3,

        /// <summary>
        /// The q1
        /// </summary>
        Q1 = 4,

        /// <summary>
        /// The q2
        /// </summary>
        Q2 = 5,

        /// <summary>
        /// The q3
        /// </summary>
        Q3 = 6,

        /// <summary>
        /// The q4
        /// </summary>
        Q4 = 7,
    }

    /// <summary>
    /// Frequency Enumeration
    /// </summary>
    public enum Frequency
    {
        /// <summary>
        /// The annual
        /// </summary>
        Annual = 1,

        /// <summary>
        /// The half yearly
        /// </summary>
        HalfYearly = 2,

        /// <summary>
        /// The quarterly
        /// </summary>
        Quarterly = 3,
    }

    /// <summary>
    /// PayoutType Enumeration
    /// </summary>
    public enum PayoutType
    {
        /// <summary>
        /// The discrete
        /// </summary>
        Discrete = 1,

        /// <summary>
        /// The cumulative
        /// </summary>
        Cumulative = 2,
    }

    /// <summary>
    /// PayoutCurveType Enumeration
    /// </summary>
    public enum PayoutCurveType
    {
        /// <summary>
        /// The multiplier
        /// </summary>
        Multiplier = 1,

        /// <summary>
        /// The direct
        /// </summary>
        Direct = 2,

        /// <summary>
        /// The linear
        /// </summary>
        Linear = 3,

        /// <summary>
        /// The flat
        /// </summary>
        Flat = 4,
    }

    /// <summary>
    /// DB Save Exceptions
    /// </summary>
    public enum DBSaveExceptions
    {
        /// <summary>
        /// The delete prevented. tuples which are not allowed to delete from code will return this error
        /// </summary>
        DeletePrevented = -5,

        /// <summary>
        /// The edit prevented. tuples which are not allowed to edit or delete from code will return this error
        /// </summary>
        [Display(Name = "edit prevented")]
        EditPrevented = -4,

        /// <summary>
        /// The duplicate
        /// </summary>
        [Display(Name = "duplicate")]
        Duplicate = -3,

        /// <summary>
        /// The updated by other user/
        /// </summary>
        [Display(Name = "updated by other user")]
        UpdatedByOtherUser = -2,

        /// <summary>
        /// The deleted by other user
        /// </summary>
        [Display(Name = "deleted by other user")]
        DeletedByOtherUser = -1,
    }

    /// <summary>
    /// UserSaveExceptions enum
    /// </summary>
    public enum UserSaveExceptions
    {
        /// <summary>
        /// Input Empty
        /// </summary>
        InputEmpty = -1,

        /// <summary>
        /// LookUp NotFound
        /// </summary>
        LookUpNotFound = -2,

        /// <summary>
        /// Invalid SizeOfFarm
        /// </summary>
        InvalidSizeOfFarm = -3,

        /// <summary>
        /// Invalid Date
        /// </summary>
        InvalidDate = -4,

        /// <summary>
        /// The country mismatch
        /// </summary>
        CountryMisMatch = -5,

        /// <summary>
        /// The updated
        /// </summary>
        Duplicate = 1,

        /// <summary>
        /// The saved
        /// </summary>
        Saved = 2
    }

    /// <summary>
    /// Document Type enum
    /// </summary>
    public enum DocumentType
    {
        /// <summary>
        /// The user document
        /// </summary>
        UserDocument = 1,

        /// <summary>
        /// The country document
        /// </summary>
        CountryDocument = 2
    }
}
