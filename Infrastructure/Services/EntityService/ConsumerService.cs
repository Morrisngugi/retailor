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
    public class ConsumerService : BaseEntityService<Consumer>, IConsumerService
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IBaseEntityRepository<Consumer> _baseEntityRepository;

        public ConsumerService(IBaseEntityRepository<Consumer> baseEntityRepository,IConsumerRepository consumerRepository) : base(baseEntityRepository)
        {
            _consumerRepository = consumerRepository;
            _baseEntityRepository = baseEntityRepository;
        }



        public async Task<ServiceResponse<Consumer>> Create(AddConsumerRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Merchant>>> GetMerchants(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Consumer>> Update(Guid Id, UpdateConsumerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
