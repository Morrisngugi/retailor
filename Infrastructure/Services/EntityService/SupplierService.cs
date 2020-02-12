using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using Core.Services.EntityServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityService
{
    public class SupplierService : BaseEntityService<Supplier>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IBaseEntityRepository<Supplier> _baseEntityRepository;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public SupplierService(ICodeGeneratorService codeGeneratorService,IBaseEntityRepository<Supplier> baseEntityRepository, ISupplierRepository supplierRepository) : base(baseEntityRepository)
        {
            _supplierRepository = supplierRepository;
            _baseEntityRepository = baseEntityRepository;
            _codeGeneratorService = codeGeneratorService;


        }

        public async Task<ServiceResponse<Supplier>> Create(AddSuplierRequest request)
        {
            try
            {
                var supplier = await _supplierRepository.FindOneByConditions(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
                if (supplier != null)
                {
                    return new ServiceResponse<Supplier>($"The Supplier with name {request.Name} already exist");
                }

                var now = DateTime.Now;
                var newSupplier = new Supplier
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
                    BaseEntityId = newSupplier.Id
                };

                var contactPerson = new ContactPerson
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    ContactPhone = request.ContactPhone,
                    ContactEmail = request.ContactEmail,
                    Code = $"CNTP{_codeGeneratorService.GenerateRandomString(8)}",
                    BaseEntityId = newSupplier.Id
                };

                var setting = new Setting
                {
                    Code = $"STNG{_codeGeneratorService.GenerateRandomString(8)}",
                    Name = request.SettingName,
                    Description = request.SettingDescription,
                    PurchaseOrderAutoApproval = request.PurchaseOrderAutoApproval,
                    SaleOrderAutoApproval = request.SaleOrderAutoApproval,
                    BaseEntityId = newSupplier.Id
                };

                newSupplier.Address = address;
                //newSupplier.AddressId = address.Id;
                newSupplier.ContactPerson = contactPerson;
                //newSupplier.ContactPersonId = contactPerson.Id;
                newSupplier.Setting = setting;
                //newSupplier.SettingId = setting.Id;


                await _supplierRepository.Create(newSupplier);
                return new ServiceResponse<Supplier>(newSupplier);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Supplier>($"An Error Occured While Creating The Anchor Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<Anchor>>> GetAnchors(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Merchant>>> GetMerchants(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Supplier>> Update(Guid Id, UpdateSupplierRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
