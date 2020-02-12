using Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.ProductViewModel
{
    public class AddProductViewModel
    {
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
        public Guid SubBrandId { get; set; }
        public Guid PackagingTypeId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public Guid VatId { get; set; }
        [DisplayName("Stock Type")]
        public StockType StockType { get; set; }

    }
}
