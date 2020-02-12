using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Vat
{
    public class AddVatViewModel
    {
        public Guid VatCategoryId { get; set; }
        [DisplayName("Rate (%)")]
        [Required(ErrorMessage = "Vat Rate is required")]
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
