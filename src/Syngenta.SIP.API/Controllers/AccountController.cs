// ***********************************************************************
// <copyright file="AccountController.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace SyngentaSIP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Swashbuckle.Swagger.Annotations;
    using Syngenta.SIP.API;
    using Syngenta.SIP.Models;
    using Syngenta.SIP.Models.ViewModels;

    /// <summary>
    /// class AccountController
    /// </summary>
    /// <seealso cref="SyngentaSIP.API.Controllers.BaseController" />
    [SyngentaSIPAPIAuthorizeAttribute]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : BaseController
    {
        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <returns>
        /// File response
        /// </returns>
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ResponseModel))]
        public HttpResponseMessage GetUserInfo()
        {
            UserModel user = this.UserService.GetUserDetails(this.UserId);
            List<SalaryDetailViewModel> salaryDetailsList = new List<SalaryDetailViewModel>();
            if (user.CurentRole != null)
            {
                var role = user.CurentRole;
                if (!user.Country.IsSalaryEditable)
                {
                    salaryDetailsList = this.UserService.GetSalaryDetails(user);
                }
                else
                {
                    salaryDetailsList.Add(this.UserService.GetEditableSalaryDetails(user));
                }
                
                return Request.CreateResponse(
                  HttpStatusCode.OK,
                  new
                  {
                      BasicDetails = new
                      {
                          UserName = user.Name,
                          CurrentRole = role?.Role.Name,
                          Country = user.Country?.Name.Trim(),
                          Currency = user.Country?.Currency.Code,
                          DefaultLanguage = user.Language?.Name.Trim(),
                          BussinessUnitName = user.BusinessUnit.Name,
                          PlanName = user.CurentPlan.Name,
                          PlanStartDate = user.CurentPlan.StartDate,
                          PlanEndDate = user.CurentPlan.EndDate,
                          LastLoggedIn = user.LastLogin,
                          LanguageID = user.LanguageId,
                          UserId = user.Id,
                          UserADID = user.LoginID,
                          IsSalaryEditable = user.Country.IsSalaryEditable,
                          IsGoalEditable = user.Country.IsGoalEditable

                      },
                      SalaryDetails = salaryDetailsList,
                      Plan = this.UserService.GetMeasures(user),
                      Language = this.UserService.GetLanguageList(),
                      PlanYears = this.UserService.GetPlanYears(user.CurentRole.RoleId)
                  });
            }
            else
            {
                return Request.CreateResponse(
                 HttpStatusCode.OK,
                 new
                 {
                     BasicDetails = new
                     {
                         UserName = user.Name,
                         Country = user.Country?.Name.Trim(),
                         Currency = user.Country?.Currency.Code,
                         DefaultLanguage = user.Language?.Name.Trim(),
                         BussinessUnitName = user.BusinessUnit.Name,
                         LastLoggedIn = user.LastLogin,
                         LanguageID = user.LanguageId,
                         UserId = user.Id,
                         UserADID = user.LoginID,
                         IsSalaryEditable = user.Country.IsSalaryEditable,
                         IsGoalEditable = user.Country.IsGoalEditable
                     },
                     SalaryDetails = salaryDetailsList,
                     Plan = new { },
                     Language = this.UserService.GetLanguageList(),
                     PlanYears = this.UserService.GetPlanYears(user.CurentRole.RoleId)
                 });
            }
        }

        /// <summary>
        /// Gets the simulation list.
        /// </summary>
        /// <returns>returns simulation list</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlanViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(PlanViewModel))]
        public HttpResponseMessage GetSimulationList()
        {
            UserModel user = this.UserService.GetUserDetails(this.UserId);
            return Request.CreateResponse(HttpStatusCode.OK, this.UserService.GetSimulations(user));
        }

        /// <summary>
        /// Saves the basic details.
        /// </summary>
        /// <param name="basicDetailViewModel">The basic detail view model.</param>
        /// <returns> returns resonse message</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BasicDetailViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(BasicDetailViewModel))]
        public HttpResponseMessage SaveBasicDetails(BasicDetailViewModel basicDetailViewModel)
        {
            basicDetailViewModel.UserId = this.UserId;
            if (ModelState.IsValid)
            {
                var isRecordSaved = false;
                isRecordSaved = this.UserService.SaveBasicDetails(basicDetailViewModel);
                if (isRecordSaved)
                {
                    return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new ResponseModel()
                    {
                        TimeStamp = DateTime.UtcNow,
                        Message = "Successfully saved"
                    });
                }
            }
            else
            {
                return Request.CreateResponse(
                      HttpStatusCode.BadRequest,
                      new ResponseModel()
                      {
                          TimeStamp = DateTime.UtcNow,
                          Message = "User Data id inaquate"
                      });
            }

            return Request.CreateResponse(
                  HttpStatusCode.ExpectationFailed,
                  new ResponseModel()
                  {
                      TimeStamp = DateTime.UtcNow,
                      Message = "user Basic details failed"
                  });
        }

        /// <summary>
        /// Saves the salary details.
        /// </summary>
        /// <param name="salaryDetailViewModel">The salary detail view model.</param>
        /// <returns>returns response as ok when saved</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(SalaryDetailViewModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(SalaryDetailViewModel))]
        [HttpPost]
        public HttpResponseMessage SaveSalaryDetails(List<SalaryDetailViewModel> salaryDetailViewModel)
        {
            salaryDetailViewModel[0].UserId = this.UserId;
            if (ModelState.IsValid)
            {
                var isRecordSaved = false;
                isRecordSaved = this.UserService.SaveSalaryDetails(salaryDetailViewModel);
                if (isRecordSaved)
                {
                    return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new ResponseModel()
                    {
                        TimeStamp = DateTime.UtcNow,
                        Message = "Successfully saved"
                    });
                }
            }
            else
            {
                return Request.CreateResponse(
                      HttpStatusCode.BadRequest,
                      new ResponseModel()
                      {
                          TimeStamp = DateTime.UtcNow,
                          Message = "User Data id inaquate"
                      });
            }

            return Request.CreateResponse(
                  HttpStatusCode.ExpectationFailed,
                  new ResponseModel()
                  {
                      TimeStamp = DateTime.UtcNow,
                      Message = "user salary details failed"
                  });
        }

        /// <summary>
        /// Saves the last login.
        /// </summary>
        /// <returns>
        /// returns response as ok when saved
        /// </returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(int))]
        [HttpPost]
        public HttpResponseMessage SaveLastLogin()
        {
            var isRecordSaved = 0;
            isRecordSaved = this.UserService.SaveLastLoggedIn(this.UserId);
            if (isRecordSaved > 0)
            {
                return Request.CreateResponse(
                HttpStatusCode.OK,
                new ResponseModel()
                {
                    TimeStamp = DateTime.UtcNow,
                    Message = "Successfully saved"
                });
            }

            return Request.CreateResponse(
                  HttpStatusCode.ExpectationFailed,
                  new ResponseModel()
                  {
                      TimeStamp = DateTime.UtcNow,
                      Message = "user last login update failed"
                  });
        }

        /// <summary>
        /// Saves the measures details.
        /// </summary>
        /// <param name="measures">The measures.</param>
        /// <returns>returns response ok if save success</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlanViewModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(PlanViewModel))]
        [HttpPost]
        public HttpResponseMessage SaveMeasuresDetails(PlanViewModel measures)
        {
            measures.UserId = this.UserId;
            string status = this.UserService.SaveMeasuresDetails(measures, false);
            return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new ResponseModel()
                   {
                       TimeStamp = DateTime.UtcNow,
                       Message = status
                   });
        }

        /// <summary>
        /// Gets the expected payout details.
        /// </summary>
        /// <returns>returns payout details</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlanViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(PlanViewModel))]
        public HttpResponseMessage GetExpectedPayoutDetails()
        {
            UserModel user = this.UserService.GetUserDetails(this.UserId);
            return Request.CreateResponse(
                 HttpStatusCode.OK,
                 new
                 {
                     Plan = this.UserService.GetMeasures(user)
                 });
        }

        /// <summary>
        /// Saves the simulation details.
        /// </summary>
        /// <param name="measures">The measures.</param>
        /// <returns>returns ok as response if success</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlanViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(PlanViewModel))]
        public HttpResponseMessage SaveSimulationDetails(PlanViewModel measures)
        {
            measures.UserId = this.UserId;
            var result = this.UserService.SaveMeasuresDetails(measures, true);
            return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new ResponseModel()
                    {
                        IsSuccess = result == Constants.Success,
                        TimeStamp = DateTime.UtcNow,
                        ResponseCode = result,
                        Message = "Saved Successfully"
                    });
        }

        /// <summary>
        /// Saves the simulation details.
        /// </summary>
        /// <param name="measures">The measures.</param>
        /// <returns>returns ok as response if success</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlanViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(PlanViewModel))]
        public HttpResponseMessage SaveSimulations(List<PlanViewModel> measures)
        {
            var simulationsList = this.UserService.GetSimulations(this.UserService.GetUserDetails(this.UserId));
            var newSimulations = simulationsList.Select(x => x.SimulationId).ToList().Except(measures.Select(y => y.SimulationId).ToList()).ToList();

            if (measures.Any())
            {
                if (newSimulations.Any())
                {
                    foreach (var item in newSimulations)
                    {
                        var result = this.UserService.DeleteSimulationById(item);
                    }
                }

                foreach (var item in measures.Select(x => x).Where(x => x.SimulationId == 0).ToList())
                {
                    item.UserId = this.UserId;
                    var status = this.UserService.SaveMeasuresDetails(item, true);
                    if (status == Constants.SimulationCodes.SimulationsMaximumCountReached)
                    {
                        return Request.CreateResponse(
                   HttpStatusCode.NotAcceptable,
                   new ResponseModel()
                   {
                       IsSuccess = false,
                       TimeStamp = DateTime.UtcNow,
                       ResponseCode = status,
                       Message = "Simulations Maximum Count Reached"
                   });
                    }
                    if (status == Constants.SimulationCodes.SimulationsNameAlreadyExists)
                    {
                        return Request.CreateResponse(
                   HttpStatusCode.Ambiguous,
                   new ResponseModel()
                   {
                       IsSuccess = false,
                       TimeStamp = DateTime.UtcNow,
                       ResponseCode = status,
                       Message = item.SimulationName + " Name Already Exists"
                   });
                    }
                }
                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new ResponseModel()
                   {
                       IsSuccess = true,
                       TimeStamp = DateTime.UtcNow,
                       ResponseCode = Constants.Success,
                       Message = "Saved Successfully"
                   });
            }
            else
            {
                foreach (var item in simulationsList)
                {
                    var result = this.UserService.DeleteSimulationById(item.SimulationId);
                }

                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new ResponseModel()
                   {
                       IsSuccess = true,
                       TimeStamp = DateTime.UtcNow,
                       ResponseCode = Constants.Success,
                       Message = "Saved Successfully"
                   });
            }

        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        ///  The <see cref="string" />
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        public string Index()
        {
            return "Welcome to Syngenta. v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// Deletes the simulation.
        /// </summary>
        /// <param name="planViewModel">The plan view model.</param>
        /// <returns>returns ok as response if success</returns>
        [HttpPost]
        public HttpResponseMessage DeleteSimulation(PlanViewModel planViewModel)
        {
            var result = this.UserService.DeleteSimulationById(planViewModel.SimulationId);
            return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new ResponseModel()
                   {
                       IsSuccess = result == Constants.Success,
                       TimeStamp = DateTime.UtcNow,
                       ResponseCode = result
                   });
        }

        /// <summary>
        /// Gets the payout history.
        /// </summary>
        /// <returns>returns ok as response if success</returns>
        [HttpGet]
        public HttpResponseMessage GetPayoutHistory()
        {
            UserModel user = this.UserService.GetUserDetails(this.UserId);
            return Request.CreateResponse(
                  HttpStatusCode.OK,
                  new
                  {
                      PayoutHistory = this.UserService.GetPayoutHistory(user)
                  });
        }

        /// <summary>
        /// Currents the year end date.
        /// </summary>
        /// <returns>
        /// <see cref="DateTime"/>
        /// </returns>
        private DateTime CurrentYearEndDate()
        {
            string lastdayofyear = new DateTime(DateTime.Now.Year, 12, 31).ToString();
            return Convert.ToDateTime(lastdayofyear);
        }
    }
}
