using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdateGroupRequest
    {
        [Required]
        public Guid GroupId { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }

        public string Description { get; set; }

        [Required]
        public GroupType GroupType { get; set; }

        [Required]
        public Guid FactoryId { get; set; }
    }
}
