using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Country : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Region> Regions { get; set; } = new List<Region>();
    }
}
