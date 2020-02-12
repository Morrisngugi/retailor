using Core.Models;
using System;
using Core.Models.Requests;
using Core.Services.Communications;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IProductService : IBaseService<Product>
    {
        Task<ServiceResponse<Product>> Create(AddProductRequest request);
        Task<ServiceResponse<Product>> Update(Guid Id, UpdateProduct request);
        string GenerateCode(int length);
    }
}
