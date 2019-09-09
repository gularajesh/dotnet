// ***********************************************************************
// <copyright file="DataController.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

/// <summary>
/// DataController
/// </summary>
namespace SyngentaSIP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OpenIdConnect;
    using Microsoft.Practices.Unity;
    using Syngenta.SIP.API;
    using Syngenta.SIP.Implementation.Service;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// DataController class
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SyngentaSIPAdminAuthorizeAttribute]
    public class DataController : Controller
    {
        /// <summary>
        /// The user repository
        /// </summary>
        public readonly IDataService DataService;

        /// <summary>
        /// The user service
        /// </summary>
        public readonly IUserService UserService;

        /// <summary>
        /// The crypto service
        /// </summary>
        public readonly ICryptoService CryptoService;

        /// <summary>
        /// The storage service
        /// </summary>
        public readonly IStorageService StorageService;

        /// <summary>
        /// The application setting service
        /// </summary>
        public readonly IApplicationSettingService ApplicationSettingService;

        /// <summary>
        /// The log service
        /// </summary>
        public readonly ILogService LogService;

        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataController"/> class.
        /// </summary>
        public DataController()
        {
            this.DataService = UnityConfig.UnityContainer.Resolve<IDataService>();
            this.UserService = UnityConfig.UnityContainer.Resolve<IUserService>();
            this.CryptoService = UnityConfig.UnityContainer.Resolve<ICryptoService>();
            this.StorageService = UnityConfig.UnityContainer.Resolve<IStorageService>();
            this.ApplicationSettingService = UnityConfig.UnityContainer.Resolve<IApplicationSettingService>();
            this.syngentaSIPUnitOfWork = UnityConfig.UnityContainer.Resolve<ISyngentaSIPUnitOfWork>();
            this.LogService = UnityConfig.UnityContainer.Resolve<ILogService>();
        }

        /// <summary>
        /// Manages the data import.
        /// </summary>
        /// <returns>Returns Action Method</returns>
        [SyngentaSIPAdminAuthorizeAttribute]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <returns>returns result</returns>
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.GenerateKey)]
        public JsonResult GenerateKey()
        {
            var cryptoKey = this.CryptoService.GenerateKey();
            string result = this.ApplicationSettingService.SaveApplicationSetting(cryptoKey);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Plans this instance.
        /// </summary>
        /// <returns>
        /// Plan view
        /// </returns>
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.ReadPlan, Permissions.EditPlan)]
        public ActionResult Plan()
        {
            CountryModel countryModel = new CountryModel();
            ViewBag.Countries = new SelectList(this.UserService.GetCountriesList(), "Id", "Name");          
            return this.View("Plan", countryModel);
        }

        /// <summary>
        /// Uploads the data import.
        /// </summary>
        /// <param name="fileInput">The file input.</param>
        /// <returns>
        /// Return json result
        /// </returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditPlan)]
        public ActionResult UploadPlan(HttpPostedFileBase fileInput)
        {
            int headerRow = 0;
            var responseList = new List<ResponseModel>();
            ExcelService excelService = new ExcelService();
            if (Request.Files.Count > 0)
            {
                var filedata = Request.Files[0];
                if (Path.GetExtension(filedata.FileName).ToUpper() != ".xlsx".ToUpper())
                {
                  ViewBag.Message = "Invalid format Please Upload the file with .xlsx";
                    return this.Plan();
                }
                else
                {
                    DataTable excelDataImportCountry;
                    DataTable excelDataIncentivePayout;
                    DataTable excelDataPayoutCurve;
                    DataTable excelDataRole;
                    DataTable excelDataImportMeasure;
                    try
                    {
                        excelDataImportCountry = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "DataImportCountry");
                        excelDataIncentivePayout = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "IncentivePayout");
                        excelDataPayoutCurve = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "PayoutCurve");
                        excelDataRole = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "Role");
                        excelDataImportMeasure = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "DataImportMeasure");

                        this.DataService.ImportCountry(excelDataImportCountry);
                        this.DataService.ImportIncentivePayout(excelDataIncentivePayout);
                        this.DataService.ImportPayoutCurve(excelDataPayoutCurve);
                        this.DataService.ImportRole(excelDataRole);
                        this.DataService.ImportMeasure(excelDataImportMeasure);
                        ViewBag.Message = "File Uploaded Sucessfully";
                    }
                    catch (Exception ex)
                    {
                        string uniqueId = this.LogService.LogError(ex.Message, ex);
                        ViewBag.Message = "Failed Uploading Refer this ReferenceId-" + uniqueId;
                    }
                }
            }
            else
            {
                ViewBag.Message = "Please upload a file to continue";
            }

            return this.Plan();
        }

        /// <summary>
        /// Downloads the plan.
        /// </summary>
        /// <param name="countryModel">The country model.</param>
        /// <returns>
        /// Download view
        /// </returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.ReadPlan)]
        public ActionResult DownloadPlan(CountryModel countryModel)
        {
            MemoryStream ms = new MemoryStream();
            this.DataService.GenerateDataFile(ms, countryModel.Id);
            return this.File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PlanData.xlsx");
        }

        /// <summary>
        /// Plans the details.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>redirects to</returns>
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.ReadPlan)]
        public ActionResult DownloadPlanData(int? countryId)
        {
            countryId = countryId == null ? 0 : countryId;
            MemoryStream ms = new MemoryStream();
            this.DataService.GenerateDataFile(ms, countryId.Value);
            return this.File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PlanData.xlsx");
        }

        /// <summary>
        /// Users this instance.
        /// </summary>
        /// <returns>return user view</returns>
        /// [HttpGet]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditUser)]
        public new ActionResult User()
        {
            CountryModel countryModel = new CountryModel();           
            ViewBag.Countries = new SelectList(this.UserService.GetCountriesList(), "Id", "Name");
            return this.View("User",countryModel);
        }

        /// <summary>
        /// Uploads the user.
        /// </summary>
        /// <param name="fileInput">The file input.</param>
        /// <returns> return user view </returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditUser)]
        public ActionResult UploadUser(HttpPostedFileBase fileInput)
        {
            int headerRow = 0;
            var responseList = new List<ResponseModel>();
            ExcelService excelService = new ExcelService();
            if (Request.Files.Count > 0)
            {
                var filedata = Request.Files[0];
                if (Path.GetExtension(filedata.FileName).ToUpper() != ".xlsx".ToUpper())
                {
                    ViewBag.Message = "Invalid format Please Upload the file with .xlsx";
                    return this.User();
                }
                else
                {
                    DataTable excelDataImportUser, excelDataImportGoal, excelDataImportSalary, excelDataImportPayoutHistory;
                    try
                    {
                        excelDataImportUser = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "User");
                        excelDataImportSalary = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "UserSalary");
                        excelDataImportGoal = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "Goal Amount");
                        excelDataImportPayoutHistory = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "PayOutHistoryNew");
                        this.DataService.ImportUser(excelDataImportUser);
                        this.DataService.SalaryImport(excelDataImportSalary);
                        this.DataService.ImportGoal(excelDataImportGoal);
                        this.DataService.ImportPayoutHistory(excelDataImportPayoutHistory);
                        ViewBag.Message = "User Data Uploaded Sucessfully";
                    }
                    catch (Exception ex)
                    {
                        string uniqueId = this.LogService.LogError(ex.Message, ex);
                        ViewBag.Message = "Failed Uploading Refer this ReferenceId-" + uniqueId;
                    }
                }
            }
            else
            {
                ViewBag.Message = "Please upload a file to continue";
            }

            return this.User();
        }

        /// <summary>
        /// Downloads all data.
        /// </summary>
        /// <returns>returns view</returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.ReadUser)]
        public ActionResult DownloadAllData(CountryModel countryModel)
        {
            ViewBag.Message = string.Empty;
            MemoryStream ms = new MemoryStream();
            this.DataService.GenerateUserDataFile(ms, countryModel.Id);
            return this.File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UserData.xlsx");
        }

        /// <summary>
        /// Uploads the salary.
        /// </summary>
        /// <returns>returns view</returns>
        [HttpGet]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditSalary)]
        public ActionResult Salary()
        {
            return this.View("Salary");
        }

        /// <summary>
        /// Salaries the upload.
        /// </summary>
        /// <param name="fileInput">The file input.</param>
        /// <returns>
        /// returns result
        /// </returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditSalary)]
        public ActionResult UploadSalary(HttpPostedFileBase fileInput)
        {
            int headerRow = 0;
            var responseModel = new List<ResponseModel>();
            ExcelService excelService = new ExcelService();
            if (Request.Files.Count > 0)
            {
                var filedata = Request.Files[0];
                if (Path.GetExtension(filedata.FileName).ToUpper() != ".xlsx".ToUpper())
                {
                    return this.ViewBag.Message = "Invalid format";
                }
                else
                {
                    DataTable excelData;
                    try
                    {
                        excelData = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "Sheet1");
                        this.DataService.SalaryImport(excelData);
                        ViewBag.Message = "Salary Uploaded SuccessFully";
                    }
                    catch (Exception ex)
                    {
                        string uniqueId = this.LogService.LogError(ex.Message, ex);
                        ViewBag.Message = "Uploading Failed Refer this ReferenceId-" + uniqueId + " " + "in Log";
                    }
                }
            }
            else
            {
                ViewBag.Message = "Please upload a file to continue";
            }

            return this.Salary();
        }

        /// <summary>
        /// Uploads the data import.
        /// </summary>
        /// <returns> returns upload Data Import Page</returns>
        [HttpGet]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditPlan)]
        public ActionResult UploadDataImport()
        {
            return this.View();
        }

        /// <summary>
        /// Downs the load salary.
        /// </summary>
        /// <returns>Resultant View </returns>
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.ReadSalary)]
        public ActionResult DownLoadSalary()
        {
            ViewBag.Message = string.Empty;
            MemoryStream ms = new MemoryStream();
            this.DataService.GenerateSalaryDataFile(ms);
            return this.File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UserSalaryData.xlsx");
        }

        /// <summary>
        /// Goals the import.
        /// </summary>
        /// <returns> Return View</returns>
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditGoal)]
        public ActionResult Goal()
        {
            return this.View("Goal");
        }

        /// <summary>
        /// Imports the goal amount.
        /// </summary>
        /// <param name="dummyId">The dummy identifier.</param>
        /// <returns>Json Response Based on result</returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditGoal)]
        public ActionResult ImportGoalAmount(int? dummyId)
        {
            int headerRow = 0;
            var responseList = new List<ResponseModel>();
            ExcelService excelService = new ExcelService();
            if (Request.Files.Count > 0)
            {
                var filedata = Request.Files[0];
                if (Path.GetExtension(filedata.FileName).ToUpper() != ".xlsx".ToUpper())
                {
                  ViewBag.Message = "Invalid format";
                    return this.Goal();
                }
                else
                {
                    DataTable excelDataImportGoal;
                    try
                    {
                        excelDataImportGoal = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "Goal Amount");
                        this.DataService.ImportGoal(excelDataImportGoal);
                        ViewBag.Message = "Goal Amount File Uploaded Sucessfully";
                    }
                    catch (Exception ex)
                    {
                        string uniqueId = this.LogService.LogError(ex.Message, ex);
                        ViewBag.Message = "Failed Uploading Refer this ReferenceId-" + uniqueId;
                    }
                }
            }
            else
            {
                ViewBag.Message = "Please upload a file to continue";
            }

            return this.Goal();
        }

        /// <summary>
        /// Downs the load goal.
        /// </summary>
        /// <returns>Returns Gaol </returns>
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.ReadGoal)]
        public ActionResult DownLoadGoal()
        {
            ViewBag.Message = string.Empty;
            MemoryStream ms = new MemoryStream();
            this.DataService.GenerateGoalDataFile(ms);
            return this.File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GoalDataImport.xlsx");
        }

        /// <summary>
        /// Datas the import payout history.
        /// </summary>
        /// <returns>Action View </returns>
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditPayoutHistory)]
        public ActionResult PayoutHistory()
        {
            return this.View("PayoutHistory");
        }

        /// <summary>
        /// Uploads the payout history.
        /// </summary>
        /// <param name="dummyId">The dummy identifier.</param>
        /// <returns>
        /// Json Response Based on result
        /// </returns>
        [HttpPost]
        [SyngentaSIPAdminAuthorizeAttribute(Permissions.EditPayoutHistory)]
        public ActionResult UploadPayoutHistory(int? dummyId)
        {
            int headerRow = 0;
            var responseList = new List<ResponseModel>();
            ExcelService excelService = new ExcelService();
            if (Request.Files.Count > 0)
            {
                var filedata = Request.Files[0];
                if (Path.GetExtension(filedata.FileName).ToUpper() != ".xlsx".ToUpper())
                {
                    ViewBag.Message = "Invalid format";
                    return this.PayoutHistory(); 
                }
                else
                {
                    DataTable excelDataImportPayoutHistory;
                    try
                    {
                        excelDataImportPayoutHistory = excelService.ToDataTable(filedata.InputStream, headerRow, headerRow + 1, "PayOutHistoryNew");
                        this.DataService.ImportPayoutHistory(excelDataImportPayoutHistory);
                        ViewBag.Message = "Payout History  File Uploaded Sucessfully";
                    }
                    catch (Exception ex)
                    {
                        string uniqueId = this.LogService.LogError(ex.Message, ex);
                        ViewBag.Message = "Failed Uploading Refer this ReferenceId-" + uniqueId;
                    }
                }
            }
            else
            {
                ViewBag.Message = "Please upload a file to continue";
            }

            return this.PayoutHistory();
        }
    }
}