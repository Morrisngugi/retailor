using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IProductDiscountService : IBaseService<ProductDiscount>
    {
        Task<ServiceResponse<ProductDiscount>> Update(Guid Id, UpdateProductDiscountRequest request);
        Task<ServiceResponse<ProductDiscount>> Create(AddProductDiscountRequest request);
        string GenerateCode(int length);
    }
}
