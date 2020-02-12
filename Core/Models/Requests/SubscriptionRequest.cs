using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class SubUnsubRequest
    {
        public string FactoryKeyword { get; set; }
        public string SubUnsubKeyword { get; set; }
        public string GroupName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
