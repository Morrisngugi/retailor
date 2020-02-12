using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdateSaleValueDiscountRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid TierId { get; set; }
    }
}
