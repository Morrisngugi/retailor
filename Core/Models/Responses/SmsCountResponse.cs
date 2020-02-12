using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Responses
{
    public class SmsCountResponse
    {
        public int SmsSent { get; set; }
        public int SmsPending { get; set; }
        public int SmsDelivered { get; set; }
        public int SmsFailed { get; set; }
        public int SmsReceived { get; set; }
    }
}
