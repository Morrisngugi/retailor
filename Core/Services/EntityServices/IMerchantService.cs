using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface IMerchantService : IBaseService<Merchant>
    {
        Task<ServiceResponse<List<Supplier>>> GetSuppliers(Guid entityId);
        Task<ServiceResponse<List<Consumer>>> GetConsumers(Guid entityId);
        Task<ServiceResponse<Merchant>> Update(Guid Id, UpdateMerchantRequest request);
        Task<ServiceResponse<Merchant>> Create(AddMerchantRequest request);
    }
}
