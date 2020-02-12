using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class ConsumerMerchant
    {
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public Guid ConsumerId { get; set; }
        public Consumer Consumer { get; set; }
    }
}
