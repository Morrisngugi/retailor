using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class AnchorSupplier
    {
        public Guid AnchorId { get; set; }
        public Anchor Anchor { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
