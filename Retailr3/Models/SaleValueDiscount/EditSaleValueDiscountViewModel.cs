using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.SaleValueDiscount
{
    public class EditSaleValueDiscountViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Tier Name")]
        [Required(ErrorMessage = "Tier Name is Required")]
        public Guid TierId { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
