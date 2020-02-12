using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Pricing
{
    public class AddPricingViewModel
    {
        [DisplayName("Price")]
        [Required(ErrorMessage = "Price Amount is required")]
        public decimal Price { get; set; } = 1M;
        public Guid ProuctId { get; set; }
        public Guid TierId { get; set; }

    }
}
