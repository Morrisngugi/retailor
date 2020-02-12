using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Configurations
{
    public class Provider
    {
        public string Endpoint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string KeyWord { get; set; }
        public string From { get; set; }
        public int BulkSMsMode { get; set; }
        public int RetryDuration { get; set; }
        public int LinkId { get; set; }
        public string CountryCode { get; set; }
    }
}
