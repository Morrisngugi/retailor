using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class AppConfig
    {
        public int KeySize { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NotifyrBaseUrl { get; set; }
        public string ApiManagrBaseUrl { get; set; }
        public string ApiManagerUrlSuffix { get; set; }
        public string ApimKey { get; set; }
        public string LoanType { get; set; }
        public string CurrencyRegionInfo { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string NotificationDisplayTime { get; set; }
        public string Authority { get; set; }
        public string SmsUrl { get; set; }
        public string SmsRate { get; set; }
    }
}
