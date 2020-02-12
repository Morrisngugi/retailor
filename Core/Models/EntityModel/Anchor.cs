using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Anchor : BaseEntity
    {
        public IList<AnchorSupplier> AnchorSuppliers { get; set; } = new List<AnchorSupplier>();
    }
}
