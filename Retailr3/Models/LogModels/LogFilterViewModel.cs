using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.LogModels
{
    public class LogFilterViewModel
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
