using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Services;
using Core.Services.Communications;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository<EwsLog> _logRepository;

        public LogService(ILogRepository<EwsLog> logRepository)
        {
            _logRepository = logRepository;
        }
        public async Task<ServiceResponse<EwsLog>> FindAllLogById(int id)
        {
            try
            {
                var logResource = await _logRepository.GetById(id);

                if (logResource == null)
                {
                    return new ServiceResponse<EwsLog>($"The Requested Log Could not be Found");
                }
                return new ServiceResponse<EwsLog>(logResource);
            }
            catch (Exception ex)
            {
                Log.Error("An Error Occured While fetching the requested Log Resource. {@ex}", ex);
                return new ServiceResponse<EwsLog>($"An Error Occured While fetching the requested Log Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<EwsLog>>> FindAllLogs()
        {
           
            try
            {
                var regionResource = await _logRepository.FindAll();
                return new ServiceResponse<List<EwsLog>>(regionResource);
            }
            catch (Exception ex)
            {
                Log.Error("An Error Occured While fetching the requested Resource. {@ex}", ex);
                return new ServiceResponse<List<EwsLog>>($"An Error Occured While fetching the requested Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<EwsLog>>> FindLogsByConditions(LogFilterRequest request)
        {
            try
            {
                var expresion = LogExpression(request);
                var regionResource = await _logRepository.FindByConditions(expresion);
                return new ServiceResponse<List<EwsLog>>(regionResource);
            }
            catch (Exception ex)
            {
                Log.Error("An Error Occured While fetching the requested Logs Resource. {@ex}", ex);
                return new ServiceResponse<List<EwsLog>>($"An Error Occured While fetching the requested Logs Resource. {ex.Message}");
            }
        }

        private Expression<Func<EwsLog, bool>> LogExpression(LogFilterRequest model)
        {
            var predicate = UtilityService.True<EwsLog>();

            //predicate = UtilityService.And(predicate, x => x.Level != EntityStatus.Disabled);

            if (model.Level != null)
            {
                predicate = UtilityService.And(predicate, x => x.Level.Contains(model.Level));
            }

            if (model.LogEvent != null)
            {
                predicate = UtilityService.And(predicate, x => x.LogEvent.Contains(model.LogEvent));
            }

            if (model.Message != null)
            {
                predicate = UtilityService.And(predicate, x => x.Message.Contains(model.Message));
            }

            if (model.MessageTemplate != null)
            {
                predicate = UtilityService.And(predicate, x => x.MessageTemplate.Contains(model.MessageTemplate));
            }

            if (model.StartDate != null)
            {
                predicate = UtilityService.And(predicate, x => x.TimeStamp >= model.StartDate);
            }

            if (model.EndDate != null)
            {
                predicate = UtilityService.And(predicate, x => x.TimeStamp <= model.EndDate);
            }

            if (model.Exception != null)
            {
                predicate = UtilityService.And(predicate, x => x.Exception.Contains(model.Exception));
            }

            if (model.LogEvent != null)
            {
                predicate = UtilityService.And(predicate, x => x.Exception.Contains(model.LogEvent));
            }

            return predicate;
        }
    }
}
