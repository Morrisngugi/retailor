using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class UnitOfMeasureType : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<UnitOfMeasure> UnitOfMeasures { get; set; } = new List<UnitOfMeasure>();
    }
}
