using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.RegionModels
{
    public class EditRegionViewModel
    {
        [DisplayName("Region Id")]
        [Required(ErrorMessage = "Region Id is Required")]
        public Guid RegionId { get; set; }

        [DisplayName("Region Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Region Name is Required")]
        public string RegionName { get; set; }

        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Country is Required")]
        public Guid CountryId { get; set; }

        [DisplayName("Region Description")]
        [StringLength(255)]
        public string RegionDescription { get; set; }
    }
}
