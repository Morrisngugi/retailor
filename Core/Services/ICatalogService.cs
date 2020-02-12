using Core.Models;
using Core.Models.EntityModel;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICatalogService : IBaseService<Catalog>
    {
        Task<ServiceResponse<Catalog>> Create(AddCatalogRequest request);
        Task<ServiceResponse<Catalog>> Update(Guid Id, UpdateCatalogRequest request);
        Task<ServiceResponse<Catalog>> AddProducts(Guid catalogId, List<Guid> productIds);
        Task<ServiceResponse<Catalog>> RemoveProducts(Guid catalogId, List<Guid> productIds);
        Task<ServiceResponse<BaseEntity>> GetCatalogEntity(Guid entityId);
        Task<bool> IsExpired(Guid catalogId);
    }
}
