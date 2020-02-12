using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class SaleValueDiscountItem : Entity
    {
        public decimal SaleValue { get; set; } = 0M;
        public decimal DiscountRate { get; set; } = 0M;
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SaleValueDiscountId { get; set; }
        public SaleValueDiscount SaleValueDiscount { get; set; }
    }
}
