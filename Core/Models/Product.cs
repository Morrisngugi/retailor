using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public decimal FactoryPrice { get; set; } = 1M;
        public string ImageUrl { get; set; }
        public decimal ReOrderLevel { get; set; } = 1M;


        public Guid SubBrandId { get; set; }
        public SubBrand SubBrand { get; set; }

        public Guid PackagingTypeId { get; set; }
        public PackagingType PackagingType { get; set; }


        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public Guid UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public Guid VatId { get; set; }
        public Vat Vat { get; set; }
        public StockType StockType { get; set; }
        public IList<CatalogProduct> CatalogProducts { get; set; } = new List<CatalogProduct>();
        public IList<Pricing> ProductPricings { get; set; } = new List<Pricing>();

    }
}
