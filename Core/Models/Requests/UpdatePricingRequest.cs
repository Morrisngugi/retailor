using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdatePricingRequest
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; } = 1M;
        public Guid TierId { get; set; }
        public Guid ProductId { get; set; }
    }
}
