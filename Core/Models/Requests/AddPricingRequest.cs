using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddPricingRequest
    {
        public decimal Price { get; set; } = 1M;
        public Guid TierId { get; set; }
        public Guid ProductId { get; set; }
    }
}
