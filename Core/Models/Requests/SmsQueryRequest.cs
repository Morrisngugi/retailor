using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class SmsQueryRequest
    {
        [Required]
        public string Code { get; set; }
        public string Month { get; set; }
    }
}
