using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public Guid RegionId { get; set; }
        public Guid FactoryId { get; set; }
        public string DisplayName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
