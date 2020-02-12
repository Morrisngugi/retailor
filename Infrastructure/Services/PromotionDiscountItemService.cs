using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PromotionDiscountItemService : BaseService<PromotionDiscountItem>, IPromotionDiscountItemService
    {
        private readonly IBaseRepository<PromotionDiscountItem> _baseRepository;

        public PromotionDiscountItemService(IBaseRepository<PromotionDiscountItem> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<ServiceResponse<PromotionDiscountItem>> Create(AddPromotionDiscountItemRequest request)
        {
            throw new NotImplementedException();
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"PRDI{random.ToUpper()}";
        }

        public async Task<ServiceResponse<PromotionDiscountItem>> Update(Guid id, UpdatePromotionDiscountItemRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<PromotionDiscountItem>($"The requested Discount Item could not be found");
                }

                result.ParentProductQuantity = request.ParentProductQuantity;
                result.FreeOfChargeQuantity = request.FreeOfChargeQuantity;
                result.DiscountRate = request.DiscountRate;
                result.EffectiveDate = request.EffectiveDate;
                result.EndDate = request.EndDate;
                result.PromotionDiscountId = request.PromotionDiscountId;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<PromotionDiscountItem>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<PromotionDiscountItem>($"An Error Occured While Updating The Discount Item. {ex.Message}");
            }
        }
    }
}
