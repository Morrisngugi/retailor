using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests.EntityRequest
{
    public class UpdateSupplierRequest : BaseEntityRequest
    {
        public Guid Id { get; set; }
    }
}
