using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface ISupplierService : IBaseService<Supplier>
    {
        Task<ServiceResponse<List<Anchor>>> GetAnchors(Guid entityId);
        Task<ServiceResponse<List<Merchant>>> GetMerchants(Guid entityId);
        Task<ServiceResponse<Supplier>> Update(Guid Id, UpdateSupplierRequest request);
        Task<ServiceResponse<Supplier>> Create(AddSuplierRequest request);
    }
}
