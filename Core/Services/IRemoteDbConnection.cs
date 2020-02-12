using Core.Models.Requests;
using Core.Models.Responses;
using Core.Services.Communications;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IRemoteDbConnection
    {
        //RemoteDbQueryResponse
        //[Queue("queryqueue")]
        //[AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        Task<ServiceResponse<List<RemoteDbQueryResponse>>> ConnectAndQuery(RemoteDbQueryRequest request);

        //[Queue("queryqueue")]
        //[AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        Task<ServiceResponse<RemoteDbQueryResponse>> ConnectAndRunSMSQuery(RemoteDbQueryRequest request);
    }
}
