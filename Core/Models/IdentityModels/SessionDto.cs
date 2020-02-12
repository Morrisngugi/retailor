using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.IdentityModels
{
    public class SessionDto
    {
        public string sub { get; set; }
        public string role { get; set; }
        public string preferred_username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}
