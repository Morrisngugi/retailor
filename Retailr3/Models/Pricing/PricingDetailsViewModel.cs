using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Pricing
{
    public class PricingDetailsViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; } = 1M;
        public Guid ProductId { get; set; }
        [DisplayName("Product")]
        public string Product { get; set; }
        public Guid TierId { get; set; }
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
