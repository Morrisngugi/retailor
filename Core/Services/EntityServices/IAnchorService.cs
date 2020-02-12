using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface IAnchorService : IBaseService<Anchor>
    {
        Task<ServiceResponse<List<Supplier>>> GetSuppliers(Guid entityId);
        Task<ServiceResponse<Anchor>> Update(Guid Id, UpdateAnchorRequest request);
        Task<ServiceResponse<Anchor>> Create(AddAnchorRequest request);
    }
}
