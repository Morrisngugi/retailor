using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface IConsumerService : IBaseService<Consumer>
    {
        Task<ServiceResponse<List<Merchant>>> GetMerchants(Guid entityId);
        Task<ServiceResponse<Consumer>> Update(Guid Id, UpdateConsumerRequest request);
        Task<ServiceResponse<Consumer>> Create(AddConsumerRequest request);
    }
}
