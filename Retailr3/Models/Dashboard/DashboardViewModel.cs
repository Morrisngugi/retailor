using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Dashboard
{
    public class DashboardViewModel
    {
        public string SmsBalance { get; set; }
        public int Groups { get; set; }
        public int Contacts { get; set; }
        public int BlackListed { get; set; }
        public int SmsSent { get; set; }
        public int SmsPending { get; set; }
        public int SmsDelivered { get; set; }
        public int SmsFailed { get; set; }
        public int SmsReceived { get; set; }
    }
}
