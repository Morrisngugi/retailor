using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.AnchorViewModels
{
    public class AnchorDetailsViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Code")]
        public string Code { get; set; }

        [DisplayName("Description")]
        [StringLength(255)]
        public string Description { get; set; }

        [DisplayName("Registration #")]
        public string RegistrationNumber { get; set; }

        [DisplayName("Licence")]
        public string LicenceNumber { get; set; }

        [DisplayName("Date of Registration")]
        public DateTime DateOfRegistration { get; set; }

        //address
        [DisplayName("Region")]
        public string RegionName { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Street")]
        public string Street { get; set; }

        [DisplayName("Address Phone")]
        public string AddressPhone { get; set; }

        [DisplayName("Address Email")]
        public string AddressEmail { get; set; }

        //contact person
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Contact Phone")]
        public string ContactPhone { get; set; }

        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }

        //setting
        [DisplayName("Setting Name")]
        public string SettingName { get; set; }

        [DisplayName("Setting Description")]
        public string SettingDescription { get; set; }

        [DisplayName("Sale Order AutoApproval")]
        public bool SaleOrderAutoApproval { get; set; }

        [DisplayName("Purchase Order AutoApproval")]
        public bool PurchaseOrderAutoApproval { get; set; }

        //tier
        [DisplayName("Tier")]
        public string TierName { get; set; }

        //entitytype
        [DisplayName("Entity Type")]
        public string EntityTypeName { get; set; }


        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
