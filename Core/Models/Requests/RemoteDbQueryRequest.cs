using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class RemoteDbQueryRequest
    {
        public string ConnectionString { get; set; }
        public string SenderId { get; set; }
        public List<string> ContactCodes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
