using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class FeedbackRequest
    {
        [Required]
        public string From { get; set; } //shortcode or senderid
        [Required]
        public string To { get; set; }//destination
        [Required]
        public string Id { get; set; }
        [Required]
        public string LinkId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Date { get; set; }
    }
}
