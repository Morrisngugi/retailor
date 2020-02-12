using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests.EntityRequest
{
    public class BaseEntityRequest
    {
        //core
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationNumber { get; set; }
        public string LicenceNumber { get; set; }
        public DateTime DateOfRegistration { get; set; }

        //address
        public Guid RegionId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string AddressPhone { get; set; }
        public string AddressEmail { get; set; }

        //contact person
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        //setting
        public string SettingName { get; set; }
        public string SettingDescription { get; set; }
        public bool SaleOrderAutoApproval { get; set; }
        public bool PurchaseOrderAutoApproval { get; set; }

        //tier
        public Guid TierId { get; set; }

        //entitytype
        public Guid? EntityTypeId { get; set; }
    }
}
