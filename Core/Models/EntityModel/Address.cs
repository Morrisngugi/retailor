using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public Guid RegionId { get; set; }
        public Region Region { get; set; }

        public Guid BaseEntityId { get; set; }
        public BaseEntity BaseEntity { get; set; }
    }
}
