using Core.Models.EntityModel;
using Core.Models.IdentityModels;
using Core.Models.Requests.EntityRequest;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using Core.Services.EntityServices;
using Infrastructure.Repositories.EntityRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityService
{
    public class AnchorService : BaseEntityService<Anchor>, IAnchorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAnchorRepository _anchorRepository;
        private readonly IPasswordGeneratorService _codeGenerator;
        private readonly ICodeGeneratorService _codeGeneratorService;
        private readonly IBaseEntityRepository<Anchor> _baseEntityRepository;

        public AnchorService(ICodeGeneratorService codeGeneratorService,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IPasswordGeneratorService codeGenerator,IAnchorRepository anchorRepository, IBaseEntityRepository<Anchor> baseEntityRepository) : base(baseEntityRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _anchorRepository = anchorRepository;
            _baseEntityRepository = baseEntityRepository;
            _codeGenerator = codeGenerator;
            _codeGeneratorService = codeGeneratorService;
        }

        public async Task<ServiceResponse<Anchor>> Create(AddAnchorRequest request)
        {
            try
            {
                var anchor = await _anchorRepository.FindOneByConditions(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
                if (anchor != null)
                {
                    return new ServiceResponse<Anchor>($"The Anchor with name {request.Name} already exist");
                }
                               
                var now = DateTime.Now;
                var newAnchor = new Anchor
                {
                    Name = request.Name,
                    Description = request.Description,
                    DateOfRegistration = request.DateOfRegistration,
                    LicenceNumber = request.LicenceNumber,
                    RegistrationNumber = request.RegistrationNumber,
                    Code = $"ANCHR{_codeGeneratorService.GenerateRandomString(8)}",
                    EntityStatus = Core.Utilities.EntityStatus.ACTIVE,
                    EntityTypeId = request.EntityTypeId,
                    TierId = request.TierId

                };

                var address = new Address
                {
                    Code = $"ADDR{_codeGeneratorService.GenerateRandomString(8)}",
                    City = request.City,
                    RegionId = request.RegionId,
                    EmailAddress = request.AddressEmail,
                    PhoneNumber = request.AddressPhone,
                    BaseEntityId = newAnchor.Id
                };

                var contactPerson = new ContactPerson
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    ContactPhone = request.ContactPhone,
                    ContactEmail = request.ContactEmail,
                    Code = $"CNTP{_codeGeneratorService.GenerateRandomString(8)}",
                    BaseEntityId = newAnchor.Id
                };

                var setting = new Setting
                {
                    Code = $"STNG{_codeGeneratorService.GenerateRandomString(8)}",
                    Name = request.SettingName,
                    Description = request.SettingDescription,
                    PurchaseOrderAutoApproval = request.PurchaseOrderAutoApproval,
                    SaleOrderAutoApproval = request.SaleOrderAutoApproval,
                    BaseEntityId = newAnchor.Id
                };

                newAnchor.Address = address;
                //newAnchor.AddressId = address.Id;
                newAnchor.ContactPerson = contactPerson;
                //newAnchor.ContactPersonId = contactPerson.Id;
                newAnchor.Setting = setting;
                //newAnchor.SettingId = setting.Id;


                await _anchorRepository.Create(newAnchor);
                return new ServiceResponse<Anchor>(newAnchor);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Anchor>($"An Error Occured While Creating The Anchor Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<Supplier>>> GetSuppliers(Guid entityId)
        {
            try
            {
                var result = await _anchorRepository.GetSuppliers(entityId);
                if (result.Count > 0)
                {
                    return new ServiceResponse<List<Supplier>>(result);
                }
                else
                {
                    return new ServiceResponse<List<Supplier>>($"No Suppliers were found for the provided anchor");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Supplier>>($"An Error Occured while fetching suppliers. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Anchor>> Update(Guid id, UpdateAnchorRequest request)
        {
            try
            {
                var anchor = await _anchorRepository.GetByIdInclusive(id, x => x.Include(w => w.ContactPerson).Include(s => s.Setting).Include(p => p.Address).ThenInclude(r => r.Region).ThenInclude(c => c.Country));
                if (anchor == null)
                {
                    return new ServiceResponse<Anchor>($"The Requested Anchor Resource Could not be found");
                }
                var updatedTime = DateTime.Now;
                anchor.Name = request.Name;
                anchor.Description = request.Description;
                anchor.LicenceNumber = request.LicenceNumber;
                anchor.RegistrationNumber = request.RegistrationNumber;
                anchor.DateOfRegistration = request.DateOfRegistration;
                anchor.LastUpdated = updatedTime;

                anchor.Setting.Name = request.SettingName;
                anchor.Setting.Description = request.SettingDescription;
                anchor.Setting.SaleOrderAutoApproval = request.SaleOrderAutoApproval;
                anchor.Setting.PurchaseOrderAutoApproval = request.PurchaseOrderAutoApproval;
                anchor.Setting.LastUpdated = updatedTime;

                anchor.ContactPerson.FirstName = request.FirstName;
                anchor.ContactPerson.LastName = request.LastName;
                anchor.ContactPerson.ContactPhone = request.ContactPhone;
                anchor.ContactPerson.ContactEmail = request.ContactEmail;

                anchor.Address.RegionId = request.RegionId;
                anchor.Address.PhoneNumber = request.AddressPhone;
                anchor.Address.Street = request.Street;
                anchor.Address.City = request.City;
                anchor.Address.EmailAddress = request.AddressEmail;

                await _anchorRepository.Update(id, anchor);
                return new ServiceResponse<Anchor>(anchor);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Anchor>($"An Error Occured while updating anchor resource. {ex.Message}");
            }


            
        }
    }
}
