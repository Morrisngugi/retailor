using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.CatalogViewModels
{
    public class EditCatalogViewModel
    {
        public Guid CatalogId { get; set; }
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
        public DateTime EffectiveDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "End Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}"/*"{0:MM/dd/yyyy}"*/, ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DisplayName("Published")]
        public bool Published { get; set; }


        [DisplayName("Entity")]
        [Required(ErrorMessage = "Entity is Required")]
        public Guid EntityId { get; set; }
    }
}
