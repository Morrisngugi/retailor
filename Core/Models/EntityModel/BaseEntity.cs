using Core.Models.EntityModels;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModel
{
    public class BaseEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationNumber { get; set; }
        public string LicenceNumber { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public EntitySize Size { get; set; }

        //public Guid ContactPersonId { get; set; }
        public ContactPerson ContactPerson { get; set; }

        //public Guid SettingId { get; set; }
        public Setting Setting { get; set; }

        //public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public Guid? EntityTypeId { get; set; }
        public EntityType EntityType { get; set; }

        public Guid TierId { get; set; }
        //public Guid? TierId { get; set; }
        //public Tier Tier { get; set; }



        //public IList<Catalog> Catalogs { get; set; } = new List<Catalog>();

    }
}
