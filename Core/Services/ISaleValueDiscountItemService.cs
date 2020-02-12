using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISaleValueDiscountItemService : IBaseService<SaleValueDiscountItem>
    {
        Task<ServiceResponse<SaleValueDiscountItem>> Update(Guid Id, UpdateSaleValueDiscountItemRequest request);
        Task<ServiceResponse<SaleValueDiscountItem>> Create(AddSaleValueDiscountItemRequest request);
        string GenerateCode(int length);
    }
}
