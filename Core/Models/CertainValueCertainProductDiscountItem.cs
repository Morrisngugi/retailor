using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class CertainValueCertainProductDiscountItem : Entity
    {
        public string Description { get; set; }
        public decimal Value { get; set; } = 0M;
        public decimal Quantity { get; set; } = 0M;
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid CertainValueCertainProductDiscountId { get; set; }
        public CertainValueCertainProductDiscount CertainValueCertainProductDiscount { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
