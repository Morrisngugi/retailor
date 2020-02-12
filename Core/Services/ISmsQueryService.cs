using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Requests;
using Hangfire;

namespace Core.Services
{
    public interface ISmsQueryService
    {
        [Queue("subunsubqueue")]
        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        Task SmsQuery(SmsQueryRequest request);
    }
}
