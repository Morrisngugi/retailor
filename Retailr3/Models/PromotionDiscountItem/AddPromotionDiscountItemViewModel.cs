using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.PromotionDiscountItem
{
    public class AddPromotionDiscountItemViewModel
    {
        public Guid PromotionDiscountId { get; set; }
        [DisplayName("Parent Product Quantity")]
        public decimal ParentProductQuantity { get; set; } = 0M;
        [DisplayName("Free Of Charge Quantity")]
        public decimal FreeOfChargeQuantity { get; set; } = 0M;
        [DisplayName("Discount Rate")]
        public decimal DiscountRate { get; set; } = 0M;
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}
