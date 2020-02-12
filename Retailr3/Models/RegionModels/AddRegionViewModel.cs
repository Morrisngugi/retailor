using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Retailr3.Models.RegionModels
{
    
    public class AddRegionViewModel
    {
        [DisplayName("Region Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Region Name is Required")]
        public string Name { get; set; }

        [DisplayName("Country Name")]
        [Required(ErrorMessage = "Country is Required")]
        public Guid CountryId { get; set; }

        [DisplayName("Region Description")]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
