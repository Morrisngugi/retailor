using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests.EntityRequest
{
    public class UpdateConsumerRequest : BaseEntityRequest
    {
        public Guid Id { get; set; }
    }
}
