using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPromotionDiscountService : IBaseService<PromotionDiscount>
    {
        Task<ServiceResponse<PromotionDiscount>> Update(Guid Id, UpdatePromotionDiscountRequest request);
        Task<ServiceResponse<PromotionDiscount>> Create(AddPromotionDiscountRequest request);
        string GenerateCode(int length);
    }
}
