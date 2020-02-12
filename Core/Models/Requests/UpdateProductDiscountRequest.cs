using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class UpdateProductDiscountRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CatalogueId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid PackagingId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
