using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.AnchorViewModels
{
    public class AddAnchorViewModel
    {
        [DisplayName("Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Anchor Name is Required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(255)]
        public string Description { get; set; }

        [DisplayName("Registration #")]
        [StringLength(50)]
        [Required(ErrorMessage = "Reg # is Required")]
        public string RegistrationNumber { get; set; }

        [DisplayName("Licence")]
        [StringLength(50)]
        [Required(ErrorMessage = "LicenceNumber is Required")]
        public string LicenceNumber { get; set; }

        [DisplayName("Date of Registration")]
        [Required(ErrorMessage = "Registration Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfRegistration { get; set; }

        //address
        [DisplayName("Region")]
        [Required(ErrorMessage = "Region is Required")]
        public Guid RegionId { get; set; }

        [DisplayName("City")]
        [StringLength(50)]
        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }

        [DisplayName("Street")]
        [StringLength(50)]
        [Required(ErrorMessage = "Street is Required")]
        public string Street { get; set; }

        [DisplayName("Address Phone")]
        [StringLength(20)]
        [Required(ErrorMessage = "Address Phone is Required")]
        public string AddressPhone { get; set; }

        [DisplayName("Address Email")]
        [StringLength(60)]
        [Required(ErrorMessage = "Address Email is Required")]
        public string AddressEmail { get; set; }

        //contact person
        [DisplayName("First Name")]
        [StringLength(30)]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(30)]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [DisplayName("Contact Phone")]
        [StringLength(20)]
        [Required(ErrorMessage = "Contact Phone is Required")]
        public string ContactPhone { get; set; }

        [DisplayName("Contact Email")]
        [StringLength(60)]
        [Required(ErrorMessage = "Contact Email is Required")]
        public string ContactEmail { get; set; }

        //setting
        [DisplayName("Name")]
        [StringLength(20)]
        [Required(ErrorMessage = "Setting Name is Required")]
        public string SettingName { get; set; }

        [DisplayName("Description")]
        [StringLength(255)]
        [Required(ErrorMessage = "Setting Description is Required")]
        public string SettingDescription { get; set; }

        [DisplayName("Sale Order Auto-Approval")]
        public bool SaleOrderAutoApproval { get; set; }

        [DisplayName("Purchase Order Auto-Approval")]
        public bool PurchaseOrderAutoApproval { get; set; }

        //tier
        [DisplayName("Tier")]
        [Required(ErrorMessage = "Tier is Required")]
        public Guid TierId { get; set; }

        //entitytype
        [DisplayName("Entity Type")]
        [Required(ErrorMessage = "Entity Type is Required")]
        public Guid EntityTypeId { get; set; }


    }
}
