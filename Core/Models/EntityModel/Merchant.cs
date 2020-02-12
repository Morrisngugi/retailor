using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Merchant : BaseEntity
    {
        public IList<MerchantSupplier> MerchantSuppliers { get; set; } = new List<MerchantSupplier>();
        public IList<ConsumerMerchant> ConsumerMerchants { get; set; } = new List<ConsumerMerchant>();
    }
}
