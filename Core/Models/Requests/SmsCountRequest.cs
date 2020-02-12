using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class SmsCountRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
