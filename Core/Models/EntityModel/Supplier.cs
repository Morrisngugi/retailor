using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Supplier : BaseEntity
    {
        public IList<MerchantSupplier> MerchantSuppliers { get; set; } = new List<MerchantSupplier>();
        public IList<AnchorSupplier> AnchorSuppliers { get; set; } = new List<AnchorSupplier>();
        
    }
}
