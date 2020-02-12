using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.Tier
{
    public class TierDetailsViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Tier Name")]
        public string Name { get; set; }
        [DisplayName("Tier Code")]
        public string Code { get; set; }
        [DisplayName("Tier Description")]
        public string Description { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
