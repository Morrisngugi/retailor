using Core.Models;
using Core.Models.Configurations;
using Core.Models.Notification;
using Core.Models.Requests;
using Core.Repositories;
using Core.Services;
using Core.Services.Communications;
using Core.Utilities;
using Hangfire;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using Serilog;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SmsSenderJob : ISmsSenderJob
    {
        private readonly IRemoteDbConnection _remoteQuery;
        private readonly IOptions<SmsProvider> _smsConfig;
        private readonly IPasswordGeneratorService _passwordGeneratorService;

        public SmsSenderJob(IOptions<SmsProvider> smsConfig,IRemoteDbConnection remoteQuery, IPasswordGeneratorService passwordGeneratorService)
        {
            _passwordGeneratorService = passwordGeneratorService;
            _remoteQuery = remoteQuery;
            _smsConfig = smsConfig;
        }

        [Queue("sendingqueuedsms")]
        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void SendQueuedSms(SendQueuedSmsRequest smsRequest)
        {
            Log.Error("Started sms sending job");
            Log.Error("Message {@smsRequest} sent successfully", smsRequest);
            var endPoint = _smsConfig.Value.AfricasTalking.Endpoint + "/messaging";
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    client.Headers.Add("apiKey", _smsConfig.Value.AfricasTalking.Password);
                    client.Headers.Add("accept", "application/json");
                    var values = new NameValueCollection
                    {
                        {"message", smsRequest.Message},
                        {"to", FormatRecipients(smsRequest.Recipients)},
                        {"username", _smsConfig.Value.AfricasTalking.Username},
                        {"from", _smsConfig.Value.AfricasTalking.From}
                    };

                    var responsebytes = client.UploadValues(endPoint, "POST", values);
                    var result = Encoding.UTF8.GetString(responsebytes);
                    var africasTalkingResponse = JsonConvert.DeserializeObject<SendSmsResponse>(result);
                    Log.Error("Ended sms sending job");

                    
                    var addSmsRequest = new AddSmsRequest
                    {
                        Message = smsRequest.Message,
                        MessageId = africasTalkingResponse.SMSMessageData.Recipients[0].messageId,
                        Recipient = FormatRecipient(africasTalkingResponse.SMSMessageData.Recipients[0].number),
                        MessageType = smsRequest.MessageType,
                        SmsBillingType = (smsRequest.MessageType.Equals(MessageType.BULK) || smsRequest.MessageType.Equals(MessageType.SINGLE) || smsRequest.MessageType.Equals(MessageType.MONTHLYSMS)) ? SmsBillingType.FACTORY : SmsBillingType.CONTACT,
                        SmsCategory = SmsCategory.OUTBOX,
                        SmsStatus = africasTalkingResponse.SMSMessageData.Recipients[0].status.Equals("success") ? SmsStatus.SUCCESS : SmsStatus.SENT
                    };

                    Log.Error("Message {@addSmsRequest} sent successfully", addSmsRequest);
                    //BackgroundJob.Enqueue(() => _smsService.AddSms(addSmsRequest));
                }

            }
            catch (WebException ex)
            {
                Log.Error($"An Error Occured While Sending the sms {ex.Message}");
            }
        }
        
   
        [Queue("sendingsms")]
        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public ServiceResponse<SendSmsResponse> SendSmsAync(SendSmsRequest smsRequest)
        {
            Log.Error("Started sms sending job");
            Log.Error("Message {@smsRequest} sent successfully", smsRequest);
            var endPoint = _smsConfig.Value.AfricasTalking.Endpoint + "/messaging";
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    client.Headers.Add("apiKey", _smsConfig.Value.AfricasTalking.Password);
                    client.Headers.Add("accept", "application/json");
                    var values = new NameValueCollection
                    {
                        {"message", smsRequest.Message},
                        {"to", FormatRecipients(smsRequest.Recipients)},
                        {"username", _smsConfig.Value.AfricasTalking.Username},
                        {"from", _smsConfig.Value.AfricasTalking.From}
                    };

                    var responsebytes = client.UploadValues(endPoint, "POST", values);
                    var result = Encoding.UTF8.GetString(responsebytes);
                    var africasTalkingResponse = JsonConvert.DeserializeObject<SendSmsResponse>(result);

                    //initiate the saving process
                    if (africasTalkingResponse.SMSMessageData.Recipients.Count > 0)
                    {
                        foreach (var recipient in africasTalkingResponse.SMSMessageData.Recipients)
                        {
                            var addSmsRequest = new AddSmsRequest
                            {
                                Message = smsRequest.Message,
                                MessageId = africasTalkingResponse.SMSMessageData.Recipients[0].messageId,
                                Recipient = FormatRecipient(recipient.number),
                                MessageType = smsRequest.MessageType,
                                SmsBillingType = (smsRequest.MessageType.Equals(MessageType.BULK) || smsRequest.MessageType.Equals(MessageType.SINGLE) || smsRequest.MessageType.Equals(MessageType.MONTHLYSMS)) ? SmsBillingType.FACTORY : SmsBillingType.CONTACT,
                                SmsCategory = SmsCategory.OUTBOX,
                                SmsStatus = recipient.status.Equals("success") ? SmsStatus.SUCCESS : SmsStatus.SENT
                            };

                            //BackgroundJob.Enqueue(() => _smsService.AddSms(addSmsRequest));
                        }
                    }
                    Log.Error("Ended sms sending job");
                    return new ServiceResponse<SendSmsResponse>(africasTalkingResponse);
                }

            }
            catch (WebException ex)
            {
                Log.Error($"An Error Occured While Sending the sms {ex.Message}");
                return new ServiceResponse<SendSmsResponse>($"An Error Occured While Sending the sms {ex.Message}");
            }
        }

        [Queue("sendingsubunsubsms")]
        [AutomaticRetry(Attempts = 10, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void SendSubUnsubSms(SendSmsRequest smsRequest)
        {
            Log.Error("Started sms sending job");
            Log.Error("Message {@smsRequest} sent successfully", smsRequest);
            var endPoint = _smsConfig.Value.AfricasTalking.Endpoint + "/messaging";
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    client.Headers.Add("apiKey", _smsConfig.Value.AfricasTalking.Password);
                    client.Headers.Add("accept", "application/json");
                    var values = new NameValueCollection
                    {
                        {"message", smsRequest.Message},
                        {"to", FormatRecipients(smsRequest.Recipients)},
                        {"username", _smsConfig.Value.AfricasTalking.Username},
                        {"from", _smsConfig.Value.AfricasTalking.From}
                    };

                    var responsebytes = client.UploadValues(endPoint, "POST", values);
                    var result = Encoding.UTF8.GetString(responsebytes);
                    var africasTalkingResponse = JsonConvert.DeserializeObject<SendSmsResponse>(result);

                    //initiate the saving process
                    if (africasTalkingResponse.SMSMessageData.Recipients.Count > 0)
                    {
                        foreach (var recipient in africasTalkingResponse.SMSMessageData.Recipients)
                        {
                            var addSmsRequest = new AddSmsRequest
                            {
                                Message = smsRequest.Message,
                                MessageId = africasTalkingResponse.SMSMessageData.Recipients[0].messageId,
                                Recipient = FormatRecipient(recipient.number),
                                MessageType = smsRequest.MessageType,
                                SmsBillingType = (smsRequest.MessageType.Equals(MessageType.BULK) || smsRequest.MessageType.Equals(MessageType.SINGLE) || smsRequest.MessageType.Equals(MessageType.MONTHLYSMS)) ? SmsBillingType.FACTORY : SmsBillingType.CONTACT,
                                SmsCategory = SmsCategory.OUTBOX,
                                SmsStatus = recipient.status.Equals("success") ? SmsStatus.SUCCESS : SmsStatus.SENT
                            };

                            //BackgroundJob.Enqueue(() => _smsService.AddSms(addSmsRequest));
                        }
                    }
                    Log.Error("Ended sms sending job");
                }

            }
            catch (WebException ex)
            {
                Log.Error($"An Error Occured While Sending the sms {ex.Message}");
            }
        }


        #region Methods
        private string FormatRecipients(List<string> recipients)
        {
            var receiver = "";
            string to = "";
            foreach (var address in recipients)
            {
                if (address.StartsWith("+"))
                {
                    receiver = address.Substring(1);
                }
                else if (address.StartsWith("0"))
                {
                    receiver = _smsConfig.Value.AfricasTalking.CountryCode + address.Substring(1);
                }
                else
                {
                    receiver = address;
                }

                to += "," + receiver;
            }
            if (to.StartsWith(","))
            {
                to = to.Substring(1);
            }

            return to;
        }
        private string FormatRecipient(string address)
        {
            var receiver = "";
            string to = "";

            if (address.StartsWith("+"))
            {
                receiver = address.Substring(1);
            }
            else if (address.StartsWith("0"))
            {
                receiver = _smsConfig.Value.AfricasTalking.CountryCode + address.Substring(1);
            }
            else
            {
                receiver = address;
            }

            to += receiver;
            return to;
        }

       


        #endregion
    }
}
