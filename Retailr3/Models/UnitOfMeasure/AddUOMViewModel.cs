using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.UnitOfMeasure
{
    public class AddUOMViewModel
    {
        public Guid UomTypeId { get; set; }
        [DisplayName("Unit Of Measure Name")]
        [Required(ErrorMessage = "Unit of Measure Name is required")]
        public string Name { get; set; }
        [DisplayName("Unit Of Measure Description")]
        public string Description { get; set; }

        [DisplayName("Grammage/Unit")]
        [Range(1, int.MaxValue, ErrorMessage = "Grammage should be greater than 0")]
        public decimal Grammage { get; set; }

        [DisplayName("Pack Size")]
        [Range(1, int.MaxValue, ErrorMessage = "Pack Size should be greater than 0")]
        public decimal PackSize { get; set; }

        [DisplayName("Pallet Size")]
        [Range(1, int.MaxValue, ErrorMessage = "Pallet Size should be greater than 0")]
        public decimal PalletSize { get; set; }
    }
}
