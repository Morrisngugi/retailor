using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.CountryModels
{
    public class ListCountryViewModel
    {
        [DisplayName("Id")]
        public Guid CountryId { get; set; }

        [DisplayName("Country")]
        public string CountryName { get; set; }

        [DisplayName("Code")]
        public string CountryCode { get; set; }


        [DisplayName("Description")]
        public string CountryDescription { get; set; }

        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
