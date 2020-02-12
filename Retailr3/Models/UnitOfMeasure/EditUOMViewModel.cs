using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.UnitOfMeasure
{
    public class EditUOMViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Unit Of Measure Type Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
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

        public Guid UOMTypeId { get; set; }
        [DisplayName("UOM Type Name")]
        public string UOMTypeName { get; set; }
    }
}
