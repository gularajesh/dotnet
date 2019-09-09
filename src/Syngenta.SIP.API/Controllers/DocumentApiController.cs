// ***********************************************************************
// <copyright file="DocumentApiController.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace SyngentaSIP.API.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Syngenta.SIP.API;
    using Syngenta.SIP.Models;

    /// <summary>
    /// class DocumentApiController
    /// </summary>
    /// <seealso cref="SyngentaSIP.API.Controllers.BaseController" />
    [SyngentaSIPAPIAuthorizeAttribute]
    public class DocumentApiController : BaseController
    {
        /// <summary>
        /// Users the docuement.
        /// </summary>
        /// <returns>
        /// File response
        /// </returns>
        [HttpGet]
        public HttpResponseMessage UserDocuement()
        {
            var userName = this.User.Identity.Name.Split('@');
            byte[] bytearray = this.DocumentService.ReadUserDocument(userName[0]);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(bytearray);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = userName + ".pdf" };
            return result;
        }

        /// <summary>
        /// Countries the docuement.
        /// </summary>
        /// <returns>
        /// File response
        /// </returns>
        [HttpGet]
        public HttpResponseMessage CountryDocuement()
        {
            var userId = this.User.Identity.Name;
            UserModel user = this.UserService.GetUserDetails(this.UserId);
            byte[] bytearray = this.DocumentService.ReadCountryDocument(user.Country.Name);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(bytearray);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = user.Country.Name + ".pdf" };
            return result;
        }
    }
}
