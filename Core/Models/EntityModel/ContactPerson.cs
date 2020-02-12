using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class ContactPerson :  Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public Guid BaseEntityId { get; set; }
        public BaseEntity BaseEntity { get; set; }

    }
}
