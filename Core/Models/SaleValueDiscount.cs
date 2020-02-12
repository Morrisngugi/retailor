using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class SaleValueDiscount :Entity
    {
        public string Description { get; set; }
        public Guid TierId { get; set; }
        public Tier Tier { get; set; }
        public IList<SaleValueDiscountItem> SaleValueDiscountItems { get; set; } = new List<SaleValueDiscountItem>();
    }
}
