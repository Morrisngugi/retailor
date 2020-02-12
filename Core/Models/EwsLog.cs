using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models
{
    public class EwsLog
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        //public Xml Properties { get; set; }
        public string LogEvent { get; set; }
    }
}
