using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddPromotionDiscountRequest
    {
        public string Description { get; set; }
        public Guid ProductId { get; set; }
    }
}
