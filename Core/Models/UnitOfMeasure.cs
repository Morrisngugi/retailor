using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class UnitOfMeasure : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Grammage { get; set; } = 1M;
        public decimal PackSize { get; set; } = 1M;
        public decimal PalletSize { get; set; } = 1M;


        public Guid UnitOfMeasureTypeId { get; set; }
        public UnitOfMeasureType UnitOfMeasureType { get; set; }
    }
}
