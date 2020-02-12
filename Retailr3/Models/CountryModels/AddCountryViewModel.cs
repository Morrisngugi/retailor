using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.CountryModels
{
    public class AddCountryViewModel
    {
        
        [DisplayName("Country Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Country Name is Required")]
        public string Name { get; set; }

        [DisplayName("Country Description")]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
