using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class AddGroupRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }
        public GroupType GroupType { get; set; }

        [Required]
        public Guid FactoryId { get; set; }
    }
}
