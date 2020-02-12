using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Repositories.EntityRepositories;
using Core.Services.Communications;
using Core.Services.EntityServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityService
{
    public class MerchantService : BaseEntityService<Merchant>, IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IBaseEntityRepository<Merchant> _baseEntityRepository;

        public MerchantService(IBaseEntityRepository<Merchant> baseEntityRepository,IMerchantRepository merchantRepository) : base(baseEntityRepository)
        {
            _merchantRepository = merchantRepository;
            _baseEntityRepository = baseEntityRepository;
        }

        public async Task<ServiceResponse<Merchant>> Create(AddMerchantRequest request)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResponse<List<Consumer>>> GetConsumers(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Supplier>>> GetSuppliers(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Merchant>> Update(Guid Id, UpdateMerchantRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
