using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests.EntityRequest
{
    public class AddAddressRequest
    {
        public Guid RegionId { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
