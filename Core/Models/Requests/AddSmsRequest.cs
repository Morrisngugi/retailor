using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddSmsRequest
    {
        public string Message { get; set; }
        public string MessageId { get; set; }
        public SmsCategory SmsCategory { get; set; }
        public SmsStatus SmsStatus { get; set; }
        public MessageType MessageType { get; set; }
        public SmsBillingType SmsBillingType { get; set; }
        public string Recipient { get; set; }
    }
}
