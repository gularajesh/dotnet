// ***********************************************************************
// <copyright file="HomeController.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SyngentaSIP.API.Controllers
{
    using System.Web.Mvc;
    using Syngenta.SIP.API;

    /// <summary>
    /// Defines the <see cref="HomeController" />
    /// </summary>
   [SyngentaSIPAdminAuthorizeAttribute]
    public class HomeController : Controller
    {
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />
        /// </returns>
        public ActionResult Index()
        {
              return this.View();
        }
    }
}
