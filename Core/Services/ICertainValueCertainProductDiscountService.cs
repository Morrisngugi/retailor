using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICertainValueCertainProductDiscountService : IBaseService<CertainValueCertainProductDiscount>
    {
        Task<ServiceResponse<CertainValueCertainProductDiscount>> Update(Guid Id, UpdateCertainValueCertainProductDiscountRequest request);
        Task<ServiceResponse<CertainValueCertainProductDiscount>> Create(AddCertainValueCertainProductDiscount request);
        string GenerateCode(int length);
    }
}
