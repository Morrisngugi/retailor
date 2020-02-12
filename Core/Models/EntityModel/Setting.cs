using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class Setting : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool SaleOrderAutoApproval { get; set; } = false;
        public bool PurchaseOrderAutoApproval { get; set; } = false;

        public Guid BaseEntityId { get; set; }
        public BaseEntity BaseEntity { get; set; }

    }
}
