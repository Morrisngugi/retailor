using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdateRegionRequest
    {
        [Required(ErrorMessage = "Region Id is Required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Region Name is Required")]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
