using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Catalog : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(6);
        public bool Published { get; set; } = false;


        public Guid EntityId { get; set; }
        //public BaseEntity BaseEntity { get; set; }

        public IList<CatalogProduct> CatalogProducts { get; set; } = new List<CatalogProduct>();
    }
}
