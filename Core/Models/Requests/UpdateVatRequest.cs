using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdateVatRequest
    {
        public Guid Id { get; set; }
        public decimal Rate { get; set; } = 0M;
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid VatCategoryId { get; set; }
    }
}
