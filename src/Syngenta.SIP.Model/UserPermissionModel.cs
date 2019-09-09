// ***********************************************************************
// <copyright file="UserPermissionModel.cs" company="Syngenta">
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
    /// User Permission Model
    /// </summary>
    public class UserPermissionModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Key, Column(Order = 0)]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the permission identifier.
        /// </summary>
        /// <value>
        /// The permission identifier.
        /// </value>
        [Key, Column(Order = 1)]
        public int PermissionID { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        [ForeignKey(nameof(UserID))]
        public virtual UserModel Users { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        [ForeignKey(nameof(PermissionID))]
        public virtual PermissionModel Permissions { get; set; }
    }
}
