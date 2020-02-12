using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Notification
{
    public class SendSmsResponse
    {
        public SMSMessageData SMSMessageData { get; set; }
    }

    public class Recipients
    {
        public int statusCode { get; set; }
        public string number { get; set; }
        public string status { get; set; }
        public string cost { get; set; }
        public string messageId { get; set; }

    }
    public class SMSMessageData
    {
        public string Message { get; set; }
        public IList<Recipients> Recipients { get; set; }

    }
}
