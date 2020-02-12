using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests
{
    public class AddUnitOfMeasureTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UnitOfMeasureTypeId { get; set; }
    }
}
