using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Configurations;
using Core.Models.Requests;
using Core.Services;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace Retailr3.Controllers
{
    public class FeedBackController : Controller
    {

        private readonly ISmsQueryService _smsQueryService;
        private readonly IOptions<SmsProvider> _smsConfig;

        public FeedBackController(IOptions<SmsProvider> smsConfig, ISmsQueryService smsQueryService)
        {
            _smsQueryService = smsQueryService;
            _smsConfig = smsConfig;
        }

        [HttpPost]
        public async Task Callback(FeedbackRequest request)
        {
            Log.Warning("Feedback Request received as: {@request}", request);
            if (!ModelState.IsValid)
            {
                Log.Error("Invalid Callback Request");
                return;
            }
            try
            {
                

                var content = request.Text.Split(' ');

                if (content.Length > 0)
                {
                    //check for subscription or unsubscription
                    if (content.Length == 3)
                    {
                        var subUnsubRequest = new SubUnsubRequest
                        {
                            FactoryKeyword = content[0],
                            SubUnsubKeyword = content[1],
                            GroupName = content[2],
                            PhoneNumber = request.To
                        };

                        //push the request to sub/unsub queue service
                        //BackgroundJob.Enqueue(() => _contactService.SubUnsub(subUnsubRequest));
                        Log.Error("Sms Query Request {@request} submitted for processing", subUnsubRequest);
                        return;
                    }

                    //check for query request for specific month
                    if (content.Length == 2)
                    {
                        var queryRequest = new SmsQueryRequest
                        {
                            Code = content[0],
                            Month = content[1]
                        };


                        //push the request to query queue service
                        BackgroundJob.Enqueue(() => _smsQueryService.SmsQuery(queryRequest));
                        Log.Error("Sms Query Request {@request} submitted for processing", queryRequest);
                        return;
                    }

                    //check for query request for current month
                    if (content.Length == 1)
                    {
                        var queryRequest = new SmsQueryRequest
                        {
                            Code = content[0]
                        };
                        //push the request to query queue service
                        BackgroundJob.Enqueue(() => _smsQueryService.SmsQuery(queryRequest));
                        Log.Error("Sms Query Request {@request} submitted for processing", queryRequest);
                        return;
                    }
                }
                else
                {
                    Log.Error($"Invalid Callback Request @request", request);
                    return;
                }
                
               
            }
            catch(Exception ex)
            {
                Log.Error("An Error {@ex} Occoured while processing the feedback request {@request}", ex, request);
                return;
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