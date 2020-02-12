using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Pricing
{
    public class EditPricingViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Price")]
        [Required(ErrorMessage = "Price Amount is required")]
        public decimal Price { get; set; } = 1M;
        [DisplayName("Product")]
        [Required(ErrorMessage = "Product Name is required")]
        public string Product { get; set; }
        public Guid ProuctId { get; set; }
        [DisplayName("Tier")]
        [Required(ErrorMessage = "Tier Name is required")]
        public string Tier { get; set; }
        public Guid TierId { get; set; }
    }
}
