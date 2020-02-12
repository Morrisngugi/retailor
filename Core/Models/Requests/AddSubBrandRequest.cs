using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddSubBrandRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid BrandId { get; set; }
    }
}
