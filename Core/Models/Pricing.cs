using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Pricing : Entity
    {
        public decimal Price { get; set; } = 1M;

        public Guid TierId { get; set; }
        public Tier Tier { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
