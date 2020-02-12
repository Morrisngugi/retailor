using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ILogService
    {
        Task<ServiceResponse<List<EwsLog>>> FindAllLogs();
        Task<ServiceResponse<EwsLog>> FindAllLogById(int id);
        Task<ServiceResponse<List<EwsLog>>> FindLogsByConditions(LogFilterRequest request);
    }
}
