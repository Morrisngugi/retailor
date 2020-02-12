using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
   
    public class UserData
    {
        public string Balance { get; set; }
    }

    public class AccountBalanceResponse
    {
        public UserData UserData { get; set; }
    }
}
