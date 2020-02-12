using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddCatalogRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Published { get; set; }

        public Guid EntityId { get; set; }
    }
}
