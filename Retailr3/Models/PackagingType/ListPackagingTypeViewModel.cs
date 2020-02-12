using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.PackagingType
{
    public class ListPackagingTypeViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Packaging Name")]
        public string PackagingName { get; set; }
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
