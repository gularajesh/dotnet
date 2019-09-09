// ***********************************************************************
// <copyright file="SyngentaSIPAdminAuthorizeAttribute.cs" company="Syngenta">
//     Copyright ©  2017
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.API
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using SIP.Interface.Service;
    using SIP.Models;
    using SyngentaSIP.API;

    /// <summary>
    /// SyngentaSIPAPIAuthorizeAttribute class
    /// </summary>
    /// <seealso cref="System.Web.Http.AuthorizeAttribute" />
    public class SyngentaSIPAdminAuthorizeAttribute : AuthorizeAttribute 
    {
        /// <summary>
        /// The user service
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// The custom permissions
        /// </summary>
        private Permissions[] permissions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyngentaSIPAdminAuthorizeAttribute" /> class.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        public SyngentaSIPAdminAuthorizeAttribute(params Permissions[] permissions)
        {
            this.userService = UnityConfig.UnityContainer.Resolve<IUserService>();
            this.permissions = permissions;
        }

        /// <summary>
        /// Calls when an action is being authorized.
        /// </summary>
        /// <param name="authContext">The authentication context.</param>
        public override void OnAuthorization(AuthorizationContext authContext)
        {
            base.OnAuthorization(authContext);
            var userIdentity = HttpContext.Current.User.Identity;
            if (!userIdentity.IsAuthenticated)
            {
                authContext.Result = new RedirectResult("~/User/Login?returnUrl=" + authContext.HttpContext.Request.Url);
                return;
            }

            var user = this.userService.GetUserByUserName(userIdentity.Name);
            bool result = false;
            if (user == null || !user.IsActive)
            {
                authContext.Result = new RedirectResult("~/User/Unauthorized");
                return;
            }

            if (this.permissions != null && this.permissions.Length > 0)
            {
                if (user.Permissions != null && user.Permissions.Count > 0)
                {
                    foreach (var permission in this.permissions)
                    {
                        result = user.Permissions.Select(x => x.PermissionID).Contains((int)permission);
                        if (result)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                result = true;
            }

            if (!result)
            {
                authContext.Result = new RedirectResult("~/User/Unauthorized");
                return;
            }

            var identity = userIdentity as System.Security.Claims.ClaimsIdentity;
            identity.AddClaim(new System.Security.Claims.Claim("UserId", user.Id.ToString()));
            identity.AddClaim(new System.Security.Claims.Claim("UserName", user.Name.ToString()));
            identity.AddClaim(new System.Security.Claims.Claim("Permissions", string.Join(",", user.Permissions.Select(x => x.PermissionID))));
        }
    }
}
