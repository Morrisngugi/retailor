using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Vat
{
    public class ListVatViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Rate (%)")]
        public decimal Rate { get; set; }
        [DisplayName("Vat Category Name")]
        public string VatCategoryName { get; set; }
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
