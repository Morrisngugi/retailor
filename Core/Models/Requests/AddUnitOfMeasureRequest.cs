using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddUnitOfMeasureRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Grammage { get; set; }
        public decimal PackSize { get; set; }
        public decimal PalletSize { get; set; }

        public Guid UnitOfMeasureTypeId { get; set; }
    }
}
