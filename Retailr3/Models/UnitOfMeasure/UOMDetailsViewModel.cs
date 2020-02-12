using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.UnitOfMeasure
{
    public class UOMDetailsViewModel
    {
        public Guid Id { get; set; }
        public Guid UOMTypeId { get; set; }
        [DisplayName("Unit Of Measure Name")]
        public string Name { get; set; }
        [DisplayName("Unit Of Measure Type Name")]
        public string UOMTypeName { get; set; }
        [DisplayName("Unit Of Measure Code")]
        public string Code { get; set; }
        [DisplayName("Unit Of Measure Description")]
        public string Description { get; set; }
        [DisplayName("Grammage")]
        public decimal Grammage { get; set; }
        [DisplayName("Pack Size")]
        public decimal PackSize { get; set; }
        [DisplayName("Pallet Size")]
        public decimal PalletSize { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
