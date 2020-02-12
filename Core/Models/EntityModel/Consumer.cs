using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Consumer : BaseEntity
    {
        public IList<ConsumerMerchant> ConsumerMerchants { get; set; } = new List<ConsumerMerchant>();
    }
}
