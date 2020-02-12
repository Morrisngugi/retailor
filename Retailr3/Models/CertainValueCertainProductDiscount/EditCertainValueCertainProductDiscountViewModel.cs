using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.CertainValueCertainProductDiscount
{
    public class EditCertainValueCertainProductDiscountViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Brand Name is Required")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
