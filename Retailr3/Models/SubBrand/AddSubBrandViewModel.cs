using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.SubBrand
{
    public class AddSubBrandViewModel
    {
        public Guid BrandId { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Sub Brand Name is required")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
