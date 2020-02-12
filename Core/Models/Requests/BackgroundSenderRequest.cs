using System.Collections.Generic;

namespace Core.Models.Requests
{
    public class BackgroundSenderRequest
    {
        public string Message { get; set; }
        public List<string> Recipients { get; set; }
        public string SenderId { get; set; }
    }
}