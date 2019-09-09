// <copyright file="GlobalExceptionHandler.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.API
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http.Filters;
    using Microsoft.Practices.Unity;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;
    using SyngentaSIP.API;

    /// <summary>
    /// Global Exception hander
    /// </summary>
    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        /// <summary>
        /// The log service
        /// </summary>
        private ILogService logService = UnityConfig.UnityContainer.Resolve<ILogService>();

        /// <summary>
        /// Catches all global exceptions
        /// </summary>
        /// <param name="context">The context</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            string uniqueId = this.logService.LogError(context.Exception.Message, context.Exception);
            
            base.OnException(context);
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new ResponseModel
                {
                    TimeStamp = DateTime.Now,
                    Message = "Unexpected error occured. Reference Id : " + uniqueId
                });
        }
    }
}