using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class CertainValueCertainProductDiscount : Entity
    {
        public string Name { get; set; }
        public string  Description { get; set; }

        public IList<CertainValueCertainProductDiscountItem> CertainValueCertainProductDiscountItems { get; set; } = new List<CertainValueCertainProductDiscountItem>();
    }
}
