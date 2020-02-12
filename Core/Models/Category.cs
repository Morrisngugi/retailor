using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
