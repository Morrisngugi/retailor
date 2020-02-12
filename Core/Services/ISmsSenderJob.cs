using Core.Models.Notification;
using Core.Services.Communications;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISmsSenderJob
    {
        [Queue("sendingqueuedsms")]
        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        void SendQueuedSms(SendQueuedSmsRequest smsRequest);

        [Queue("sendingsms")]
        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        ServiceResponse<SendSmsResponse> SendSmsAync(SendSmsRequest smsRequest);

        [Queue("sendingsubunsubsms")]
        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        void SendSubUnsubSms(SendSmsRequest smsRequest);
    }
}
