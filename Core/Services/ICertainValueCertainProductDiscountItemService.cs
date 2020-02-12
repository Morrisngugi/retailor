using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICertainValueCertainProductDiscountItemService : IBaseService<CertainValueCertainProductDiscountItem>
    {
        Task<ServiceResponse<CertainValueCertainProductDiscountItem>> Update(Guid Id, UpdateCertainValueCertainProductDiscountItemRequest request);
        Task<ServiceResponse<CertainValueCertainProductDiscountItem>> Create(AddCertainValueCertainProductDiscountItem request);
        string GenerateCode(int length);
    }
}
