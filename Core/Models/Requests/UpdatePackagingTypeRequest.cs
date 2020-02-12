using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdatePackagingTypeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PackagingId { get; set; }
    }
}
