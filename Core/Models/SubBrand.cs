using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class SubBrand : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
