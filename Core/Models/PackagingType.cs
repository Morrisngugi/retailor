using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class PackagingType : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid PackagingId { get; set; }
        public Packaging Packaging { get; set; }
    }
}
