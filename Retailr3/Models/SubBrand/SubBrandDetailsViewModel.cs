

using System;
using System.ComponentModel;

namespace Retailr3.Models.SubBrand
{
    public class SubBrandDetailsViewModel
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Brand Name")]
        public string BrandName { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
