using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ProductDiscount : Entity
    {
        public string Name { get; set; }
        public Guid  CatalogueId { get; set; }
        public string CatalogueName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public Guid PackagingId { get; set; }
        public string PackagingName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
