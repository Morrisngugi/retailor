using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Africastalking
{
    public class AfricasTalkingSendSmsRequest
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string to { get; set; }

        [Required]
        public string message { get; set; }


        public string from { get; set; }

        public string bulkSMSMode { get; set; }

        public string enqueue { get; set; }

        public string keyword { get; set; }

        public string linkId { get; set; }

        public string retryDurationInHours { get; set; }
    }
}
