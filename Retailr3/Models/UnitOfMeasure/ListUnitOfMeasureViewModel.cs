using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.UnitOfMeasure
{
    public class ListUnitOfMeasureViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("UOM Type Name")]
        public string UOMTypeName { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("Description")]
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
