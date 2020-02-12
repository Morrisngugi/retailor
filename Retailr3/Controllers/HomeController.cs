using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Requests;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Retailr3.Helpers;
using Retailr3.Models;
using Retailr3.Models.Dashboard;
using Retailr3.Controllers;

namespace Retailr3.Controllers
{
    [SecurityHeaders]
    //[Authorize]
    [ResponseCache(CacheProfileName = "NoCache")]
    public class HomeController : BaseController
    {
        private readonly ISessionUserService _sessionUserService;
        private readonly IOptions<AppConfig> _appConfig;
        public HomeController(ISessionUserService sessionUserService, IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
            _sessionUserService = sessionUserService;
        }
        public async Task<IActionResult> Index()
        {
            var db = new DashboardViewModel();
            try
            {
                
                
            }
            catch (Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            }
            
            

            return View(db);
        }

        public async Task<JsonResult> LastOneDaySms()
        {
            var db = new DashboardViewModel();
            try
            {

                var dateRange = new SmsCountRequest
                {
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now
                };
                

            }
            catch (Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            }



            return new JsonResult(db);
        }


        public async Task<JsonResult> LastSevenDaySms()
        {
            var db = new DashboardViewModel();
            try
            {
                
                var dateRange = new SmsCountRequest
                {
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now
                };
               

            }
            catch (Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            }



            return new JsonResult(db);
        }

        public async Task<JsonResult> LastThirtyDaySms()
        {
            var db = new DashboardViewModel();
            try
            {

                var dateRange = new SmsCountRequest
                {
                    StartDate = DateTime.Now.AddDays(-30),
                    EndDate = DateTime.Now
                };
                

            }
            catch (Exception ex)
            {
                Alert($"{ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            }



            return new JsonResult(db);
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
