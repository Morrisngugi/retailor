using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Pricing
{
    public class ListPricingViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; } = 1M;
        [DisplayName("Product")]
        public string Product { get; set; }
        [DisplayName("Tier")]
        public string Tier { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
