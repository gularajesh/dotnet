// ***********************************************************************
// <copyright file="SyngentaSIPContext.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Models;

    /// <summary>
    /// SyngentaSIPContext class
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="Syngenta.SIP.Interface.Repository.ISyngentaSIPContext" />
    public class SyngentaSIPContext : DbContext, ISyngentaSIPContext
    {
        /// <summary>
        /// The command
        /// </summary>
        private SqlCommand command = new SqlCommand();

        /// <summary>
        /// The SQL data adapter
        /// </summary>
        private SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SyngentaSIPApplicationConnection"].ConnectionString);

        /// <summary>
        /// Initializes a new instance of the <see cref="SyngentaSIPContext"/> class.
        /// </summary>
        public SyngentaSIPContext() : base("SyngentaSIPApplicationConnection") 
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;            
            ////Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            ////Initialize the DB and insert master data
            Database.SetInitializer(new SyngentaSIPDBInitializer());
        }
       
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IDbSet<RoleModel> Role
        {
            get { return this.Set<RoleModel>(); }
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IDbSet<UserModel> User
        {
            get { return this.Set<UserModel>(); }
        }

        /// <summary>
        /// Gets the form types.
        /// </summary>
        /// <value>
        /// The form types.
        /// </value>
        public IDbSet<CountryModel> Country
        {
            get { return this.Set<CountryModel>(); }
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public IDbSet<CurrencyModel> Currency
        {
            get { return this.Set<CurrencyModel>(); }
        }

        /// <summary>
        /// Gets the plan.
        /// </summary>
        /// <value>
        /// The plan.
        /// </value>
        public IDbSet<PlanModel> Plans
        {
            get { return this.Set<PlanModel>(); }
        }

        /// <summary>
        /// Gets the payout curve detail.
        /// </summary>
        /// <value>
        /// The payout curve detail.
        /// </value>
        public IDbSet<PayoutCurveDetailModel> PayoutCurveDetails
        {
            get { return this.Set<PayoutCurveDetailModel>(); }
        }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>
        /// The forms.
        /// </value>
        public IDbSet<RegionModel> Region
        {
            get { return this.Set<RegionModel>(); }
        }

        /// <summary>
        /// Gets the questions.
        /// </summary>
        /// <value>
        /// The questions.
        /// </value>
        public IDbSet<TerritoryModel> Territory
        {
            get { return this.Set<TerritoryModel>(); }
        }

        /// <summary>
        /// Gets the form types.
        /// </summary>
        /// <value>
        /// The form types.
        /// </value>
        public IDbSet<CityModel> City
        {
            get { return this.Set<CityModel>(); }
        }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>
        /// The forms.
        /// </value>
        public IDbSet<BusinessUnitModel> BusinessUnit
        {
            get { return this.Set<BusinessUnitModel>(); }
        }

        /// <summary>
        /// Gets the user role details.
        /// </summary>
        /// <value>
        /// The user role details.
        /// </value>
        public IDbSet<UserRoleDetailModel> UserRoleDetails
        {
            get { return this.Set<UserRoleDetailModel>(); }
        }

        /// <summary>
        /// Gets the user salary details.
        /// </summary>
        /// <value>
        /// The user salary details.
        /// </value>
        public IDbSet<UserSalaryDetailModel> UserSalaryDetails
        {
            get { return this.Set<UserSalaryDetailModel>(); }
        }

        /// <summary>
        /// Gets the plan measures.
        /// </summary>
        /// <value>
        /// The plan measures.
        /// </value>
        public IDbSet<PlanMeasureModel> PlanMeasures
        {
            get { return this.Set<PlanMeasureModel>(); }
        }

        /// <summary>
        /// Gets the type of the payout.
        /// </summary>
        /// <value>
        /// The type of the payout.
        /// </value>
        public IDbSet<PayoutTypeModel> PayoutType
        {
            get { return this.Set<PayoutTypeModel>(); }
        }

        /// <summary>
        /// Gets the payout percentage.
        /// </summary>
        /// <value>
        /// The payout percentage.
        /// </value>
        public IDbSet<PayoutPercentageModel> PayoutPercentage
        {
            get { return this.Set<PayoutPercentageModel>(); }
        }

        /// <summary>
        /// Gets the payout percentage details.
        /// </summary>
        /// <value>
        /// The payout percentage details.
        /// </value>
        public IDbSet<PayoutPercentageDetailModel> PayoutPercentageDetails
        {
            get { return this.Set<PayoutPercentageDetailModel>(); }
        }

        /// <summary>
        /// Gets the payout curves.
        /// </summary>
        /// <value>
        /// The payout curves.
        /// </value>
        public IDbSet<PayoutCurveModel> PayoutCurves
        {
            get { return this.Set<PayoutCurveModel>(); }
        }

        /// <summary>
        /// Gets the user targets.
        /// </summary>
        /// <value>
        /// The user targets.
        /// </value>
        public IDbSet<UserTargetModel> UserTargets
        {
            get { return this.Set<UserTargetModel>(); }
        }

        /// <summary>
        /// Gets the user target details.
        /// </summary>
        /// <value>
        /// The user target details.
        /// </value>
        public IDbSet<UserTargetDetailModel> UserTargetDetails
        {
            get { return this.Set<UserTargetDetailModel>(); }
        }

        /// <summary>
        /// Gets the user target simulations.
        /// </summary>
        /// <value>
        /// The user target simulations.
        /// </value>
        public IDbSet<UserSimulationModel> UserSimulations
        {
            get { return this.Set<UserSimulationModel>(); }
        }

        /// <summary>
        /// Gets the user simulation measure details.
        /// </summary>
        /// <value>
        /// The user simulation measure details.
        /// </value>
        public IDbSet<UserSimulationMeasureDetailModel> UserSimulationMeasureDetails
        {
            get { return this.Set<UserSimulationMeasureDetailModel>(); }
        }

        /// <summary>
        /// Gets the user target simulation details.
        /// </summary>
        /// <value>
        /// The user target simulation details.
        /// </value>
        public IDbSet<UserSimulationMeasureFrequencyDetailModel> UserSimulationMeasureFrequencyDetails
        {
            get { return this.Set<UserSimulationMeasureFrequencyDetailModel>(); }
        }

        /// <summary>
        /// Gets the user simulation modifier details.
        /// </summary>
        /// <value>
        /// The user simulation modifier details.
        /// </value>
        public IDbSet<UserSimulationModifierDetailModel> UserSimulationModifierDetails
        {
            get { return this.Set<UserSimulationModifierDetailModel>(); }
        }

        /// <summary>
        /// Gets the frequency detail.
        /// </summary>
        /// <value>
        /// The frequency detail.
        /// </value>
        public IDbSet<FrequencyDetailModel> FrequencyDetail
        {
            get { return this.Set<FrequencyDetailModel>(); }
        }

        /// <summary>
        /// Gets the frequencies.
        /// </summary>
        /// <value>
        /// The frequencies.
        /// </value>
        public IDbSet<FrequencyModel> Frequencies
        {
            get { return this.Set<FrequencyModel>(); }
        }

        /// <summary>
        /// Gets the modifier.
        /// </summary>
        /// <value>
        /// The modifier.
        /// </value>
        public IDbSet<ModifierModel> Modifier
        {
            get { return this.Set<ModifierModel>(); }
        }

        /// <summary>
        /// Gets the plan modifier.
        /// </summary>
        /// <value>
        /// The plan modifier.
        /// </value>
        public IDbSet<PlanModifierModel> PlanModifier
        {
            get { return this.Set<PlanModifierModel>(); }
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public IDbSet<LanguageModel> Languages
        {
            get { return this.Set<LanguageModel>(); }
        }

        /// <summary>
        /// Gets the user payout history.
        /// </summary>
        /// <value>
        /// The user payout history.
        /// </value>
        public IDbSet<UserPayoutHistoryModel> UserPayoutHistoryDetails
        {
            get { return this.Set<UserPayoutHistoryModel>(); }
        }

        /// <summary>
        /// Gets the user permission.
        /// </summary>
        /// <value>
        /// The user permission.
        /// </value>
        public IDbSet<UserPermissionModel> UserPermission
        {
            get { return this.Set<UserPermissionModel>(); }
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public IDbSet<PermissionModel> Permission
        {
            get { return this.Set<PermissionModel>(); }
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        /// <datetime>2/11/2016 2:20 PM</datetime>
        public System.Data.IDbConnection Connection
        {
            get { return Database.Connection; }
        }

        /// <summary>
        /// Gets or Provides access to configuration options for the context.
        /// </summary>
        /// <value>
        /// An object used to access configuration options.
        /// </value>
        DbContextConfiguration ISyngentaSIPContext.Configuration
        {
            get { return this.Configuration; }
        }

        #region Entity
        /// <summary>
        /// States the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>returns state</returns>
        /// <datetime>2/11/2016 2:19 PM</datetime>
        public EntityState State<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State;
        }

        /// <summary>
        /// States the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="state">The state.</param>
        /// <returns>returns state</returns>
        /// <datetime>2/11/2016 2:19 PM</datetime>
        public EntityState State<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            return Entry(entity).State = state;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>returns state</returns>
        /// <datetime>2/11/2016 2:20 PM</datetime>
        public int Save()
        {
            const EntityState ExistingEntitiesToTrack = EntityState.Modified | EntityState.Deleted;
            const EntityState NewEntitiesToTrack = EntityState.Added;
            var changedEntities = ChangeTracker.Entries().Where(e => (e.State & ExistingEntitiesToTrack) == e.State).ToList();
            var newEntities = ChangeTracker.Entries().Where(e => (e.State & NewEntitiesToTrack) == e.State).ToList();
            return this.SaveChanges();
        }
        #endregion Entity
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <typeparam name="T">The generic</typeparam>
        /// <param name="name">Name of the SP.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>query result</returns>
        public List<T> ExecuteQuery<T>(string name, params object[] parameters)
        {
            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandText = name;
            List<T> result = new List<T>();
            try
            {
                this.Database.Connection.Open();
                cmd.Parameters.AddRange(parameters);
                using (var dr = cmd.ExecuteReader())
                {
                    var type = typeof(T);
                    if (dr.HasRows)
                    {
                        List<string> excludedProps = null;
                        var allProps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(x => (excludedProps == null || !excludedProps.Contains(x.Name)) && (x.PropertyType == typeof(string) || x.PropertyType == typeof(decimal) ||
                            (!x.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))))
                            .ToList();
                        var columns = new List<string>();

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            columns.Add(dr.GetName(i));
                        }

                        while (dr.Read())
                        {
                            T item = (T)Activator.CreateInstance(type);
                            foreach (var column in columns)
                            {
                                var prop = allProps.FirstOrDefault(x => x.Name.ToUpper() == column.ToUpper());
                                var value = dr[prop.Name];
                                var safeType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                var safeValue = value == null || value == DBNull.Value ?
                                    (safeType == typeof(string) ? string.Empty : Activator.CreateInstance(safeType)) :
                                    Convert.ChangeType(value, safeType);
                                if (value == null || value == DBNull.Value)
                                {
                                    // Do nothing if it's a nullable primitive.
                                }
                                else
                                {
                                    prop.SetValue(item, safeValue, null);
                                }
                            }

                            result.Add(item);
                        }
                    }

                    dr.Close();
                }
            }
            finally
            {
                cmd.Parameters.Clear();
                this.Database.Connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// returns data
        /// </returns>
        public DataTable GetData(string procedureName, int id)
        {
                this.connection.Open();
                DataTable ds = new DataTable();
                this.command = new SqlCommand(procedureName, this.connection);
                this.command.Parameters.AddWithValue("@CountryId", id);
                this.command.CommandType = CommandType.StoredProcedure;
                this.sqlDataAdapter = new SqlDataAdapter(this.command);
                this.sqlDataAdapter.Fill(ds);
                return ds;
        }

        /// <summary>
        /// Executes the procedure.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Result datatable from SP
        /// </returns>
        public DataTable ExecuteProcedure(string procedureName, params SqlParameter[] parameters)
        {
                this.connection.Open();
                DataTable ds = new DataTable();
                this.command = new SqlCommand(procedureName, this.connection);
                this.command.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    this.command.Parameters.AddRange(parameters);
                }

                this.sqlDataAdapter = new SqlDataAdapter(this.command);
                this.sqlDataAdapter.Fill(ds);
                return ds;
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuilder, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the Data base Model Builder and Data base Context Factory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MappingConfig.BuildCatalogueContextMappings(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
