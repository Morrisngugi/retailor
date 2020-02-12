using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Auth
{
    public class BasicAuthRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
