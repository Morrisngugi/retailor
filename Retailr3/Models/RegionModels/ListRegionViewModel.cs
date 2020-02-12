using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Retailr3.Models.RegionModels
{
    [DataContract]
    public class ListRegionViewModel
    {
        [DisplayName("Id")]
        public Guid RegionId { get; set; }

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
