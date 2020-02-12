using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Packaging : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<PackagingType> PackagingTypes { get; set; } = new List<PackagingType>();
    }
}
