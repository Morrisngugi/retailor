using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.UnitOfMeasureType
{
    public class UnitOfMeasureTypeDetailsViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Unit Of Measure Type Name")]
        public string Name { get; set; }
        [DisplayName("Unit Of Measure Type Code")]
        public string Code { get; set; }
        [DisplayName("Unit Of Measure Type Description")]
        public string Description { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
