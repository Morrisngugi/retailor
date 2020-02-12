using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Vat : Entity
    {
        public decimal Rate { get; set; } = 0M;
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid VatCategoryId { get; set; }
        public VatCategory VatCategory { get; set; }
    }
}
