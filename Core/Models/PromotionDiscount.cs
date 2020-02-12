using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
   public class PromotionDiscount : Entity
    {
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public IList<PromotionDiscountItem> PromotionDiscountItems { get; set; } = new List<PromotionDiscountItem>();

    }
}
