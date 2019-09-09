// ***********************************************************************
// <copyright file="UserController.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace SyngentaSIP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OpenIdConnect;
    using Microsoft.Practices.Unity;
    using Syngenta.SIP.API;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Models;
    
    /// <summary>
    /// UserController class
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class UserController : Controller
    {
        /// <summary>
        /// The user repository
        /// </summary>
        public readonly IUserRepository UserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        public UserController()
        {
            this.UserRepository = UnityConfig.UnityContainer.Resolve<IUserRepository>();
        }

        /// <summary>
        /// Changes the role.
        /// </summary>
        /// <returns>
        /// returns change role view
        /// </returns>
        [SyngentaSIPAdminAuthorizeAttribute]
        public ActionResult ChangeRole()
        {
            ViewBag.Roles = new List<RoleModel>();
            ViewBag.Countries = this.UserRepository.GetCountriesList().OrderBy(x => x.Name);
            ViewBag.BussinessUnit = this.UserRepository.GetBussinessList().OrderBy(x => x.Name);            
            return this.View();
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="businessUnitId">The business unit identifier.</param>
        /// <returns>
        /// returns result roles
        /// </returns>
       [SyngentaSIPAdminAuthorizeAttribute]
        public JsonResult GetRoles(int countryId, int businessUnitId)
        {
            var roles = this.UserRepository.GetAllRoles().Where(x => x.CountryId == countryId && x.BusinessUnitId == businessUnitId).ToList();
            return this.Json(roles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves the user role.
        /// </summary>
        /// <param name="bussinessunitid">The business unit id.</param>
        /// <param name="countryid">The country id.</param>
        /// <param name="roleid">The role id.</param>
        /// <returns>
        /// returns message
        /// </returns>
        [SyngentaSIPAdminAuthorizeAttribute]
        public string SaveUserRole(int bussinessunitid, int countryid, int roleid)
        {
            UserModel user = this.UserRepository.GetUserByUserName(User.Identity.Name);
            this.UserRepository.ChangeUserRole(user, bussinessunitid, countryid, roleid);
            return "updated successfully";
        }

        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// returns content
        /// </returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!Request.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    System.Uri url = new System.Uri(returnUrl);
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = url.AbsoluteUri }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
                }
                else
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
                }
            }
            else if (Request.IsAuthenticated)
            {
                return this.RedirectToAction("DownloadPlan", "Data");
            }

            return this.Content(string.Empty);
        }

        /// <summary>
        /// Unauthorizeds this instance.
        /// </summary>
        /// <returns>
        /// returns view
        /// </returns>
        [AllowAnonymous]
        public ActionResult Unauthorized()
        {
            return this.View();
        }

        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        public void SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(new AuthenticationProperties { }, OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}