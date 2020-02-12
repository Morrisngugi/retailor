using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.SubBrand
{
    public class ListSubBrandViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Sub Brand Name")]
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
