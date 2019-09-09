// ***********************************************************************
// <copyright file="ISyngentaSIPSecurityContext.cs" company="Syngenta">
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
    using Syngenta.SIP.Models;

    /// <summary>
    ///   ISyngenta SIPSecurityContext Interface for Security
    /// </summary>
    public interface ISyngentaSIPSecurityContext
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        DbContextConfiguration Configuration { get; }

        #region For Models
        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings.
        /// </value>
        IDbSet<ApplicationSettingModel> ApplicationSettings { get; }
        #endregion

        #region For Entity
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        IDbConnection Connection { get; }

        /// <summary>
        /// States the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>entity state</returns>
        EntityState State<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// States the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="state">The state.</param>
        /// <returns> entity state </returns>
        EntityState State<TEntity>(TEntity entity, EntityState state) where TEntity : class;

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>it returns number of records saved successfully</returns>
        int Save();
        #endregion
    }
}
