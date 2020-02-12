using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface IConsumerRepository : IBaseEntityRepository<Consumer>
    {
        Task<Consumer> GetByIdentityIdId(Guid id);
        Task<List<Merchant>> GetMerchants(Guid consumerId);
    }
}
