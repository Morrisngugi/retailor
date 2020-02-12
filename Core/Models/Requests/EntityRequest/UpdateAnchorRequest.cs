using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests.EntityRequest
{
    public class UpdateAnchorRequest : BaseEntityRequest
    {
        public Guid Id { get; set; }
    }
}
