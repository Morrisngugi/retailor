using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class LogFilterRequest
    {
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
    }
}
