using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.PackagingType
{
    public class EditPackagingTypeViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Packaging Type Name is required")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }

        public Guid PackagingId { get; set; }
        [DisplayName("Name")]
        public string PackagingName { get; set; }
    }
}
