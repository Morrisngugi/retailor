using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddContactRequest
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string Address { get; set; }
        public Guid GroupId { get; set; }
    }
}
