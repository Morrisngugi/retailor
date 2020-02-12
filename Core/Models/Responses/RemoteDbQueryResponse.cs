using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Responses
{
    public class RemoteDbQueryResponse
    {
        public string FarmerId { get; set; }
        public string IdNumber { get; set; }
        public string Phone { get; set; }
        public string FarmerCode { get; set; }
        public string CenterName { get; set; }
        public string FarmerName { get; set; }
        public string MonthToDate { get; set; }
        public string LatestSyncDate { get; set; }
    }
}
