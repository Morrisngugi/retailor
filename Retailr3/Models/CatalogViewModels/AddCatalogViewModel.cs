using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.CatalogViewModels
{
    public class AddCatalogViewModel
    {
        [DisplayName("Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(255)]
        public string Description { get; set; }

        [DisplayName("Effective Date")]
        [Required(ErrorMessage = "Effective Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; } = DateTime.Now;

        [DisplayName("End Date")]
        [Required(ErrorMessage = "End Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(3);

        [DisplayName("Published")]
        public bool Published { get; set; }


        [DisplayName("Entity")]
        [Required(ErrorMessage = "Entity is Required")]
        public Guid EntityId { get; set; }

    }
}
