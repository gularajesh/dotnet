// ***********************************************************************
// <copyright file="Startup.Auth.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace SyngentaSIP.API
{
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// class Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Gets the o authentication options.
        /// </summary>
        /// <value>
        /// The o authentication options.
        /// </value>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }        
    }
}
