using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Brand : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<SubBrand> SubBrands { get; set; } = new List<SubBrand>();
    }
}
