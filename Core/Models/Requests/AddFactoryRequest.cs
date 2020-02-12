using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class AddFactoryRequest
    {
        [Required(ErrorMessage = "Factory Name is Required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Factory Code is Required")]
        [StringLength(50)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Region is Required")]
        public Guid RegionId { get; set; }
    }
}
