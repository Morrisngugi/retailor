using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.CatalogViewModels
{
    public class CatalogDetailsViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Entity")]
        public string Entity { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [DisplayName("Published")]
        public string Published { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
