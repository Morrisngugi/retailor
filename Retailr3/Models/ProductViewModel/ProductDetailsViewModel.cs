using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.ProductViewModel
{
    public class ProductDetailsViewModel
    {
        public Guid id { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Desription { get; set; }
        [DisplayName("Manufacturer")]
        [Required(ErrorMessage = "Manufacturer is required")]
        public string Manufacturer { get; set; }
        [DisplayName("Factory Price")]
        [Required(ErrorMessage = "Factory Price is required")]
        public decimal FactoryPrice { get; set; } = 1M;
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        [DisplayName("ReOrder Level")]
        public decimal ReOrderLevel { get; set; } = 1M;
        public string Brand { get; set; }
        public string SubBrand { get; set; }
        public string Packaging { get; set; }
        public string PackagingType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string UnitOfMeasureType { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Vat { get; set; }
        public string VatCategory { get; set; }
        [DisplayName("Stock Type")]
        public StockType StockType { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
