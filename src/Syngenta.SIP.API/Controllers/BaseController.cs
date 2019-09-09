// ***********************************************************************
// <copyright file="BaseController.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace SyngentaSIP.API.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Web.Http;
    using Microsoft.Practices.Unity;
    using Syngenta.SIP.Interface.Service;

    /// <summary>
    /// base controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BaseController : ApiController
    {        
        /// <summary>
        /// The notification service
        /// </summary>
        public readonly IUserService UserService;

        /// <summary>
        /// The log service
        /// </summary>
        public readonly ILogService LogService;

        /// <summary>
        /// The data service
        /// </summary>
        public readonly IDataService DataService;

        /// <summary>
        /// The crypto service
        /// </summary>
        public readonly ICryptoService CryptoService;

        /// <summary>
        /// The BLOB service
        /// </summary>
        public readonly IStorageService StorageService;

        /// <summary>
        /// The application setting service
        /// </summary>
        public readonly IApplicationSettingService ApplicationSettingService;

        /// <summary>
        /// The document service
        /// </summary>
        public readonly IDocumentService DocumentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        public BaseController()
        {
            this.UserService = UnityConfig.UnityContainer.Resolve<IUserService>();
            this.LogService = UnityConfig.UnityContainer.Resolve<ILogService>();
            this.DataService = UnityConfig.UnityContainer.Resolve<IDataService>();
            this.CryptoService = UnityConfig.UnityContainer.Resolve<ICryptoService>();
            this.StorageService = UnityConfig.UnityContainer.Resolve<IStorageService>();
            this.ApplicationSettingService = UnityConfig.UnityContainer.Resolve<IApplicationSettingService>();
            this.DocumentService = UnityConfig.UnityContainer.Resolve<IDocumentService>();
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId
       {
           get
           {
               var user = this.User.Identity as ClaimsIdentity;
               var claim = user.FindFirst("UserId");
               if (claim != null)
               {
                   return Convert.ToInt32(claim.Value);
               }

               return 0;
           }
       }
    }
}