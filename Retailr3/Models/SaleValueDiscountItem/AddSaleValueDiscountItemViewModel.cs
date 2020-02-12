using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.SaleValueDiscountItem
{
    public class AddSaleValueDiscountItemViewModel
    {
        public Guid SaleValueDiscountId { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Sale Value")]
        public decimal SaleValue { get; set; } = 0M;
        [DisplayName("Discount Rate")]
        public decimal DiscountRate { get; set; } = 0M;
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}
