using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.ProductDiscount
{
    public class AddProductDiscountViewModel
    {
        [DisplayName("Discount Name")]
        [Required(ErrorMessage = "Discount Name is Required")]
        public string Name { get; set; }
        [DisplayName("Catalogue Name")]
        public Guid  CatalogueId { get; set; }
        [DisplayName("Category Name")]
        public Guid CategoryId { get; set; }
        [DisplayName("Brand Name")]
        public Guid BrandId { get; set; }
        [DisplayName("Packaging Name")]
        public Guid PackagingId { get; set; }
        [DisplayName("Product Name")]
        public Guid ProductId { get; set; }
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}
