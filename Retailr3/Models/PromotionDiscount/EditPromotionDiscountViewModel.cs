using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.PromotionDiscount
{
    public class EditPromotionDiscountViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is Required")]
        public Guid ProductId { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
