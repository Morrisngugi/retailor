using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Tier
{
    public class EditTierViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Tier Name")]
        [Required(ErrorMessage = "Tier Name is Required")]
        public string Name { get; set; }
        [DisplayName("Tier Description")]
        public string Description { get; set; }
    }
}
