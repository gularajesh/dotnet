// ***********************************************************************
// <copyright file="DocumentController.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace SyngentaSIP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Syngenta.SIP.API;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;

    /// <summary>
    /// Document Controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SyngentaSIPAdminAuthorizeAttribute]
    public class DocumentController : Controller
    {
        /// <summary>
        /// The document service
        /// </summary>
        public readonly IDocumentService DocumentService;

        /// <summary>
        /// The user service
        /// </summary>
        public readonly IUserService UserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentController" /> class.
        /// </summary>
        public DocumentController()
        {
            this.DocumentService = UnityConfig.UnityContainer.Resolve<IDocumentService>();
            this.UserService = UnityConfig.UnityContainer.Resolve<IUserService>();
        }

        /// <summary>
        /// Imports the documents.
        /// </summary>
        /// <returns>
        /// return Upload Documents
        /// </returns>
        [HttpGet]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.UploadDocument)]
        public ActionResult Document()
        {
            CountryModel countryModel = new CountryModel();
            ViewBag.Countries = new SelectList(this.UserService.GetCountriesList(), "Id", "Name");
            return this.View("Document",countryModel);
        }

        /// <summary>
        /// Uploads the document.
        /// </summary>
        /// <returns>
        /// Json result
        /// </returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.UploadDocument)]
        public ActionResult UploadCountryDocument(CountryModel countryModel)
        {
            HttpPostedFileBase document = Request.Files["Countrydocument"];
            var allowedExtensions = new[] { ".pdf", ".zip" };
            var checkextension = Path.GetExtension(document.FileName).ToLower();
            if (document != null && document.ContentLength > 0 && allowedExtensions.Contains(checkextension))
            {
                this.DocumentService.UploadCountryDocument(countryModel.Year.ToString(), string.Concat(Constants.BlobSetting.CountryStorageContainer, "/", document.FileName), document.InputStream);
                ViewBag.Message = "Country Documents Uploaded Successfully";
            }
            else
            {
                ViewBag.Message = "Please upload a file with extension .pdf or .zip";
            }

            return this.Document();
        }

        /// <summary>
        /// Uploads the user document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>
        /// returns Actionresult
        /// </returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.UploadDocument)]
        public ActionResult UploadUserDocument(HttpPostedFileBase document, CountryModel countryModel)
        {
            var allowedExtensions = new[] { ".pdf", ".zip" };
            var checkextension = Path.GetExtension(document.FileName).ToLower();
            if (document != null && document.ContentLength > 0 && allowedExtensions.Contains(checkextension))
            {
                this.DocumentService.UploadUserDocument(countryModel.Year.ToString(),string.Concat(Constants.BlobSetting.UserStorageContainer,"/",document.FileName), document.InputStream);
                ViewBag.Message = "User Documents Uploaded Successfully";
            }
            else
            {
                ViewBag.Message = "Please upload a file with extension .pdf or .zip ";
            }

            return this.Document();
        }

        /// <summary>
        /// Users the docuement.
        /// </summary>
        /// <returns>
        /// returns file
        /// </returns>
        [HttpGet]
        public ActionResult UserDocument(int year)
        {         
            var userName = this.User.Identity.Name;            
            var userData = this.UserService.GetUserByUserName(userName);
            year = (year != 0 && year.ToString().Length > 3) ? year : userData.CurentPlan.Year;
            var result = this.DocumentService.ReadUserDocument(year, userData.EmployeeId, 0, 1);
            if (result != null && result.Keys.Count > 0)
            {
                byte[] bytearray = result.Values.First();
                var fileName = result.Keys.First();
                this.DocumentService.TrackUserDocumentDownload(userData, fileName,DocumentType.UserDocument.ToString(),year);
                return this.File(bytearray, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                string message = "No Document exist with name: " + userData.EmployeeId;
                return this.Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        
        /// <summary>
        /// Countries the docuement.
        /// </summary>
        /// <returns>
        /// return file
        /// </returns>
        [HttpGet]
        public ActionResult CountryDocument(int year)
        {            
            var userName = this.User.Identity.Name;
            UserModel user = this.UserService.GetUserByUserName(userName);
            year = (year != 0 && year.ToString().Length > 3) ? year : user.CurentPlan.Year;
            var fileName = user.Country.Name + ".pdf";
            byte[] bytearray = this.DocumentService.ReadCountryDocument(year, user.Country.Name);
            if (bytearray != null)
            {
                this.DocumentService.TrackUserDocumentDownload(user, fileName, DocumentType.CountryDocument.ToString(), year);
                return this.File(bytearray, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                string message = "No Document exist with name: " + fileName;
                return this.Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Downloads all data.
        /// </summary>
        /// <param name="countryModel">The country model.</param>
        /// <returns></returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.ReadUser)]
        public ActionResult DownloadUserTrackDocument(CountryModel countryModel)
        {
            ViewBag.Message = string.Empty;
            MemoryStream ms = new MemoryStream();         
            this.DocumentService.GenerateUserTrackDocumentFile(ms, countryModel.Year, countryModel.Id);
            return this.File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DocumentsReport.xlsx");
        }
    }
}