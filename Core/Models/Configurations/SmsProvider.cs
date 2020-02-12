using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Configurations
{
    public class SmsProvider
    {
        public Provider AfricasTalking { get; set; }
        public Provider Twilio { get; set; }
    }
}
