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
    public class PromotionDiscountService : BaseService<PromotionDiscount>, IPromotionDiscountService
    {
        private readonly IBaseRepository<PromotionDiscount> _baseRepository;

        public PromotionDiscountService(IBaseRepository<PromotionDiscount> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<PromotionDiscount>> Create(AddPromotionDiscountRequest request)
        {
            try
            {
                var promotionDiscount = new PromotionDiscount
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    ProductId = request.ProductId
                };

                var exist = await _baseRepository.GetByIdAndCode(promotionDiscount.Id, promotionDiscount.Code);
                if (exist != null)
                {
                    return new ServiceResponse<PromotionDiscount>($"A Discount With the Provided Code and or Id Already Exist");
                }
                await _baseRepository.Create(promotionDiscount);
                return new ServiceResponse<PromotionDiscount>(promotionDiscount);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<PromotionDiscount>($"An Error Occured While Creating The Discount. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"PRD{random.ToUpper()}";
        }

        public async Task<ServiceResponse<PromotionDiscount>> Update(Guid id, UpdatePromotionDiscountRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<PromotionDiscount>($"The requested Discount could not be found");
                }

                result.ProductId = request.ProductId;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<PromotionDiscount>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<PromotionDiscount>($"An Error Occured While Updating The Discount. {ex.Message}");
            }
        }
    }
}
