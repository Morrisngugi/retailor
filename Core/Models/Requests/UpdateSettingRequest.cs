using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdateSettingRequest
    {
        public Guid SettingId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderId { get; set; }
        public string ShortCode { get; set; }
        public string KeyWord { get; set; }
        public string SubscriptionKeyWord { get; set; }
        public string UnSubscriptionKeyWord { get; set; }
        public string SmsUrl { get; set; }
        public string SmsBalanceUrl { get; set; }
        public string ConnectionString { get; set; }
        public bool SmsRequireApproval { get; set; }
        //public string DatasourceName { get; set; }
        //public string DbUserName { get; set; }
        //public string DbPassword { get; set; }
        public Guid FactoryId { get; set; }
    }
}
