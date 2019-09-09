// ***********************************************************************
// <copyright file="SyngentaSIPAPIAuthorizeAttribute.cs" company="Syngenta">
//     Copyright ©  2017
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.API
{
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Microsoft.Practices.Unity;
    using SIP.Interface.Service;
    using SIP.Models;
    using SyngentaSIP.API;

    /// <summary>
    /// SyngentaSIPAPIAuthorizeAttribute class
    /// </summary>
    /// <seealso cref="System.Web.Http.AuthorizeAttribute" />
    public class SyngentaSIPAPIAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// The user service
        /// </summary>
        private IUserService userService;        

        /// <summary>
        /// Initializes a new instance of the <see cref="SyngentaSIPAPIAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="customRoles">The custom roles.</param>
        public SyngentaSIPAPIAuthorizeAttribute(params Roles[] customRoles)
        {
            this.userService = UnityConfig.UnityContainer.Resolve<IUserService>();            
        }

        /// <summary>
        /// Calls when an action is being authorized.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            var userIdentity = HttpContext.Current.User.Identity;
            if (userIdentity.IsAuthenticated)
            {
                var user = this.userService.GetUserByUserName(userIdentity.Name);
                if (user != null)
                    {
                        if (!user.IsActive)
                        {
                            actionContext.Response = new HttpResponseMessage
                            {
                                StatusCode = HttpStatusCode.Forbidden,
                                Content = new StringContent("Your account have been deleted by admin. Please contact admin.")
                            };
                        }

                        var identity = userIdentity as System.Security.Claims.ClaimsIdentity;
                        identity.AddClaim(new System.Security.Claims.Claim("UserId", user.Id.ToString()));
                        identity.AddClaim(new System.Security.Claims.Claim("UserName", user.Name.ToString()));
                    }
                    else
                    {
                        actionContext.Response = new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.Forbidden,
                            Content = new StringContent("You are not registered in this app. Please contact admin.")
                        };
                    }                
            }
        }

        /// <summary>
        /// Handle Unauthorized Request
        /// </summary>
        /// <param name="actionContext">action context</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            bool isSessionExpired = false;

            if (actionContext.Request.Headers != null && actionContext.Request.Headers.Authorization != null)
            {
                isSessionExpired = true;
            }

            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = !isSessionExpired ? new StringContent("Please login to proceed.") : new StringContent("It looks like your session has been expired. Please login to proceed.")
            };
        }
    }
}
