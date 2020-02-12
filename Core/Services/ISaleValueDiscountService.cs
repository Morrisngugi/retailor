using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISaleValueDiscountService : IBaseService<SaleValueDiscount>
    {
        Task<ServiceResponse<SaleValueDiscount>> Update(Guid Id, UpdateSaleValueDiscountRequest request);
        Task<ServiceResponse<SaleValueDiscount>> Create(AddSaleValueDiscountRequest request);
        string GenerateCode(int length);
    }
}
