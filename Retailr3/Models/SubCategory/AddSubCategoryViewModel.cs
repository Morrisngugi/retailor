using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.SubCategory
{
    public class AddSubCategoryViewModel
    {
        public Guid CategoryId { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Sub Category Name is required")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
 