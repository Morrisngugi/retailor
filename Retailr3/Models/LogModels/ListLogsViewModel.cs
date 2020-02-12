using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.LogModels
{
    public class ListLogsViewModel
    {
        [DisplayName("Code")]
        public int Id { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

        [DisplayName("MessageTemplate")]
        public string MessageTemplate { get; set; }

        [DisplayName("Level")]
        public string Level { get; set; }

        [DisplayName("TimeStamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [DisplayName("Exception")]
        public string Exception { get; set; }

        //[DisplayName("Properties")]
        //public Xml Properties { get; set; }

        [DisplayName("LogEvent")]
        public string LogEvent { get; set; }
    }
}
