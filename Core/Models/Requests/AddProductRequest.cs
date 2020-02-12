using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public decimal FactoryPrice { get; set; } = 1M;
        public string ImageUrl { get; set; }
        public decimal ReOrderLevel { get; set; } = 1M;
        public Guid SubBrandId { get; set; }
        public Guid PackagingTypeId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public Guid VatId { get; set; }
        public StockType StockType { get; set; }

    }
}
