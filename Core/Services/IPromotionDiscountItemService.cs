using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Core.Services
{
     public interface IPromotionDiscountItemService : IBaseService<PromotionDiscountItem>
    {
        Task<ServiceResponse<PromotionDiscountItem>> Update(Guid Id, UpdatePromotionDiscountItemRequest request);
        Task<ServiceResponse<PromotionDiscountItem>> Create(AddPromotionDiscountItemRequest request);
        string GenerateCode(int length);
    }
}
