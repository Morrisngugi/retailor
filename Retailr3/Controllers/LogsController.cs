using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Services;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Retailr3.Models.LogModels;
using Serilog;

namespace Retailr3.Controllers
{
    public class LogsController : BaseController
    {
        private readonly ILogService _logService;
        private readonly IOptions<AppConfig> _appConfig;

        public LogsController(ILogService logService, IOptions<AppConfig> appConfig)
        {
            _logService = logService;
            _appConfig = appConfig;
        }

        // GET: Logs
        public async Task<ActionResult> Index(LogFilterViewModel vm)
        {

            List<ListLogsViewModel> logs = new List<ListLogsViewModel>();
            var result = await _logService.FindAllLogs();

            if (result.Success)
            {
                if (result.Data.Count() > 0)
                {
                    foreach (var log in result.Data)
                    {
                        logs.Add(new ListLogsViewModel
                        {
                            Id = log.Id,
                            Exception = (log.Exception?.Length > 20) ? $"{log.Exception?.Substring(0, 20)} ..." : log.Exception,
                            LogEvent = log.LogEvent,
                            Level = log.Level,
                            Message = (log.Message?.Length > 20) ? $"{log.Message?.Substring(0, 20)} ..." : log.Message,
                            MessageTemplate = (log.MessageTemplate?.Length > 20) ? $"{log.MessageTemplate?.Substring(0, 20)} ..." : log.MessageTemplate,
                            //Properties = log.Properties,
                            TimeStamp = log.TimeStamp
                        });
                    }
                }
                else
                {
                    Alert("No Logs Found", NotificationType.info, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                }

                return View(logs);
            }
            else
            {
                Alert($"{result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));

                return View(logs);
            }
        }

        // GET: Logs/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    Alert($"Invalid Request", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
            //    return View();
            //}
            try
            {

                var result = await _logService.FindAllLogById(id);
                if (result.Success)
                {
                    var log = result.Data;
                    var logResource = new LogDetailsViewModel
                    {
                        Id = log.Id,
                        Exception = log.Exception,
                        LogEvent = log.LogEvent,
                        Level = log.Level,
                        Message = log.Message,
                        MessageTemplate = log.MessageTemplate,
                        //Properties = log.Properties,
                        TimeStamp = log.TimeStamp
                    };

                    return View(logResource);
                }
                else
                {
                    Alert($"Error! : {result.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                    return View();
                }


            }
            catch (Exception ex)
            {

                Alert($"An Error Occurred While Fetching The Requested Resource. {ex.Message}", NotificationType.error, Int32.Parse(_appConfig.Value.NotificationDisplayTime));
                return View();
            }
        }

       
    }
}