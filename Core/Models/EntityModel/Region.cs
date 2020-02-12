using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Region : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public IList<Address> Addresses { get; set; } = new List<Address>();
    }
}
