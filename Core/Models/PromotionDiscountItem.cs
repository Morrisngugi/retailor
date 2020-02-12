using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class PromotionDiscountItem : Entity
    {
        public decimal ParentProductQuantity { get; set; } = 0M;
        public decimal FreeOfChargeQuantity { get; set; } = 0M;
        public decimal DiscountRate { get; set; } = 0M;
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PromotionDiscountId { get; set; }
        public PromotionDiscount PromotionDiscount { get; set; }
    }
}
