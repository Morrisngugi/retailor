using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Notification
{
    public class SendSmsRequest
    {
        public string Message { get; set; }
        public string SenderId { get; set; }
        public MessageType MessageType { get; set; }
        public List<string> Recipients { get; set; }
    }
}
