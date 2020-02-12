using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Retailr3.Models.UnitOfMeasureType
{
    public class EditUnitOfMeasureTypeViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Unit Of Measure Type Name")]
        [Required(ErrorMessage ="Unit Of Measure Type Name is required")]
        public string Name { get; set; }
        [DisplayName("Unit Of Measure Type Description")]
        public string Description { get; set; }
    }
}
