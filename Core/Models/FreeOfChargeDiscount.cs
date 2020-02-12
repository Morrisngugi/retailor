using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class FreeOfChargeDiscount : Entity
    {
        public Guid ProductId { get; set; }
        //ischecked goes here...
        public string Description { get; set; }
        public DateTime  StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
