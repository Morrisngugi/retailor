using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.RegionModels
{
    public class RegionDetailsViewModel
    {
        [DisplayName("Id")]
        public Guid RegionId { get; set; }

        public Guid CountryId { get; set; }

        [DisplayName("Country")]
        public string CountryName { get; set; }

        [DisplayName("Code")]
        public string RegionCode { get; set; }

        [DisplayName("Name")]
        public string RegionName { get; set; }

        [DisplayName("Description")]
        public string RegionDescription { get; set; }

        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
