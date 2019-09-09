// ***********************************************************************
// <copyright file="ISyngentaSIPContext.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Interface.Repository
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using Models;

    /// <summary>
    /// Interface Syngenta Context
    /// </summary>
    public interface ISyngentaSIPContext
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        DbContextConfiguration Configuration { get; }

        #region Models
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        IDbSet<UserModel> User { get; }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        IDbSet<RoleModel> Role { get; }

        /// <summary>
        /// Gets the form types.
        /// </summary>
        /// <value>
        /// The form types.
        /// </value>
        IDbSet<CityModel> City { get; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        IDbSet<LanguageModel> Languages { get; }

        /// <summary>
        /// Gets the plan.
        /// </summary>
        /// <value>
        /// The plan.
        /// </value>
        IDbSet<PlanModel> Plans { get; }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        IDbSet<CurrencyModel> Currency { get; }

        /// <summary>
        /// Gets the payout curve detail.
        /// </summary>
        /// <value>
        /// The payout curve detail.
        /// </value>
        IDbSet<PayoutCurveDetailModel> PayoutCurveDetails { get; }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>
        /// The forms.
        /// </value>
        IDbSet<BusinessUnitModel> BusinessUnit { get; }

        /// <summary>
        /// Gets the questions.
        /// </summary>
        /// <value>
        /// The questions.
        /// </value>
        IDbSet<RegionModel> Region { get; }

        /// <summary>
        /// Gets the questionnaire.
        /// </summary>
        /// <value>
        /// The questionnaire.
        /// </value>
        IDbSet<CountryModel> Country { get; }

        /// <summary>
        /// Gets the question questionnaire maps.
        /// </summary>
        /// <value>
        /// The question questionnaire maps.
        /// </value>
        IDbSet<TerritoryModel> Territory { get; }

        /// <summary>
        /// Gets the user role details.
        /// </summary>
        /// <value>
        /// The user role details.
        /// </value>
        IDbSet<UserRoleDetailModel> UserRoleDetails { get; }

        /// <summary>
        /// Gets the frequencies.
        /// </summary>
        /// <value>
        /// The frequencies.
        /// </value>
        IDbSet<FrequencyModel> Frequencies { get; }

        /// <summary>
        /// Gets the user salary details.
        /// </summary>
        /// <value>
        /// The user salary details.
        /// </value>
        IDbSet<UserSalaryDetailModel> UserSalaryDetails { get; }
        
        /// <summary>
        /// Gets the plan measures.
        /// </summary>
        /// <value>
        /// The plan measures.
        /// </value>
        IDbSet<PlanMeasureModel> PlanMeasures { get; }

        /// <summary>
        /// Gets the frequency detail.
        /// </summary>
        /// <value>
        /// The frequency detail.
        /// </value>
        IDbSet<FrequencyDetailModel> FrequencyDetail { get; }

        /// <summary>
        /// Gets the type of the payout.
        /// </summary>
        /// <value>
        /// The type of the payout.
        /// </value>
        IDbSet<PayoutTypeModel> PayoutType { get; }

        /// <summary>
        /// Gets the payout percentage.
        /// </summary>
        /// <value>
        /// The payout percentage.
        /// </value>
        IDbSet<PayoutPercentageModel> PayoutPercentage { get; }

        /// <summary>
        /// Gets the payout curves.
        /// </summary>
        /// <value>
        /// The payout curves.
        /// </value>
        IDbSet<PayoutCurveModel> PayoutCurves { get; }

        /// <summary>
        /// Gets the user targets.
        /// </summary>
        /// <value>
        /// The user targets.
        /// </value>
        IDbSet<UserTargetModel> UserTargets { get; }

        /// <summary>
        /// Gets the user target details.
        /// </summary>
        /// <value>
        /// The user target details.
        /// </value>
        IDbSet<UserTargetDetailModel> UserTargetDetails { get; }

        /// <summary>
        /// Gets the user target simulations.
        /// </summary>
        /// <value>
        /// The user target simulations.
        /// </value>
        IDbSet<UserSimulationModel> UserSimulations { get; }

        /// <summary>
        /// Gets the user simulation measure details.
        /// </summary>
        /// <value>
        /// The user simulation measure details.
        /// </value>
        IDbSet<UserSimulationMeasureDetailModel> UserSimulationMeasureDetails { get; }

        /// <summary>
        /// Gets the user target simulation details.
        /// </summary>
        /// <value>
        /// The user target simulation details.
        /// </value>
        IDbSet<UserSimulationMeasureFrequencyDetailModel> UserSimulationMeasureFrequencyDetails { get; }

        /// <summary>
        /// Gets the user simulation measure details.
        /// </summary>
        /// <value>
        /// The user simulation measure details.
        /// </value>
        IDbSet<UserSimulationModifierDetailModel> UserSimulationModifierDetails { get; }

        /// <summary>
        /// Gets the user payout history details.
        /// </summary>
        /// <value>
        /// The user payout history details.
        /// </value>
        IDbSet<UserPayoutHistoryModel> UserPayoutHistoryDetails { get; }

        /// <summary>
        /// Gets the user permission.
        /// </summary>
        /// <value>
        /// The user permission.
        /// </value>
        IDbSet<UserPermissionModel> UserPermission { get; }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        IDbSet<PermissionModel> Permission { get; }

        #endregion

        #region Entity

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        /// <author>santosh</author><datetime>2/11/2016 2:20 PM</datetime>
        IDbConnection Connection { get; }

        /// <summary>
        /// States the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The entity state</returns>
        /// <author>santosh</author><datetime>2/11/2016 2:19 PM</datetime>
        EntityState State<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// States the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="state">The state.</param>
        /// <returns>The State</returns>
        /// <author>santosh</author><datetime>2/11/2016 2:19 PM</datetime>
        EntityState State<TEntity>(TEntity entity, EntityState state) where TEntity : class;

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>The Save</returns>
        /// <author>santosh</author><datetime>2/11/2016 2:20 PM</datetime>
        int Save();
        #endregion

        #region Methods

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <typeparam name="T">The generic</typeparam>
        /// <param name="name">Name of the s p.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>returns executed query</returns>
        List<T> ExecuteQuery<T>(string name, params object[] parameters);

        /// <summary>
        /// Executes the procedure.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Result data table from SP
        /// </returns>
        DataTable ExecuteProcedure(string procedureName, params SqlParameter[] parameters);

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// returns data
        /// </returns>
        DataTable GetData(string procedureName, int id);
        #endregion
    }
}
