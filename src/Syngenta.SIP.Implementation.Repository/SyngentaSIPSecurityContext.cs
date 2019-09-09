// ***********************************************************************
// <copyright file="SyngentaSIPSecurityContext.cs" company="Syngenta">
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
    /// Syngenta SIPSecurity Context
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <seealso cref="Syngenta.SIP.Interface.Repository.ISyngentaSIPSecurityContext" />
    public class SyngentaSIPSecurityContext : DbContext, ISyngentaSIPSecurityContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyngentaSIPSecurityContext"/> class.
        /// </summary>
        public SyngentaSIPSecurityContext() : base("SyngentaSIPSecurityConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            ////Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            ////Initialize the DB and insert master data
            Database.SetInitializer(new SyngentaSIPSerureDBInitializer());
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public System.Data.IDbConnection Connection
        {
            get { return Database.Connection; }
        }

        /// <summary>
        /// Gets Provides access to configuration options for the context.
        /// </summary>
        /// <value>
        /// An object used to access configuration options.
        /// </value>
        DbContextConfiguration ISyngentaSIPSecurityContext.Configuration
        {
            get { return this.Configuration; }
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings.
        /// </value>
        public IDbSet<ApplicationSettingModel> ApplicationSettings
        {
            get { return this.Set<ApplicationSettingModel>(); }
        }

        #region Entity

        /// <summary>
        /// States the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns> Entity State</returns>
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
        /// <returns>returns Entity State</returns>
        public EntityState State<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            return Entry(entity).State = state;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>
        /// <see cref="Row effectected "/>
        /// </returns>
        public int Save()
        {
            const EntityState ExistingEntitiesToTrack = EntityState.Modified | EntityState.Deleted;
            const EntityState NewEntitiesToTrack = EntityState.Added;
            var changedEntities = ChangeTracker.Entries().Where(e => (e.State & ExistingEntitiesToTrack) == e.State).ToList();
            var newEntities = ChangeTracker.Entries().Where(e => (e.State & NewEntitiesToTrack) == e.State).ToList();
            return this.SaveChanges();
        }
        #endregion

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
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MappingConfig.BuildCatalogueSecurityMappings(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
