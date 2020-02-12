using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class VatCategory : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Vat> Vats { get; set; } = new List<Vat>();
    }
}
