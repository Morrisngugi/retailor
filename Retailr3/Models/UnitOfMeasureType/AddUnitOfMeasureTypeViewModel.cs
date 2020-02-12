using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Retailr3.Models.UnitOfMeasureType
{
    public class AddUnitOfMeasureTypeViewModel
    {
        [DisplayName("Unit Of Measure Type Name")]
        [Required(ErrorMessage ="Unit of Measure Type Name is required")]
        public string Name { get; set; }
        [DisplayName("Unit Of Measure Type Description")]
        public string Description { get; set; }
    }
}
