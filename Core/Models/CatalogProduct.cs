using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class CatalogProduct
    {
        public Guid CatalogId { get; set; }
        public Catalog Catalog { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
