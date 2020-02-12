using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdateFactoryRequest
    {
        [Required(ErrorMessage = "Factory Id is Required")]
        public Guid FactoryId { get; set; }

        [Required(ErrorMessage = "Factory Name is Required")]
        [StringLength(50)]
        public string FactoryName { get; set; }

        [Required(ErrorMessage = "Factory Code is Required")]
        [StringLength(50)]
        public string FactoryCode { get; set; }


        [Required(ErrorMessage = "Region is Required")]
        public Guid RegionId { get; set; }
    }
}
