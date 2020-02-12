using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class AddRegionRequest
    {
        [Required(ErrorMessage = "Region Name is Required")]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
