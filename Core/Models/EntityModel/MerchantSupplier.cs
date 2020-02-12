using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class MerchantSupplier
    {
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
