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
    public class SaleValueDiscountItemService : BaseService<SaleValueDiscountItem>, ISaleValueDiscountItemService
    {
        private readonly IBaseRepository<SaleValueDiscountItem> _baseRepository;

        public SaleValueDiscountItemService(IBaseRepository<SaleValueDiscountItem> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<SaleValueDiscountItem>> Create(AddSaleValueDiscountItemRequest request)
        {
            try
            {
                var saleValueDiscountItem = new SaleValueDiscountItem
                {
                    Code = GenerateCode(8),
                    SaleValue = request.SaleValue,
                    DiscountRate = request.DiscountRate,
                    EffectiveDate = request.EffectiveDate,
                    EndDate = request.EndDate,
                    SaleValueDiscountId = request.SaleValueDiscountId
                };

                var exist = await _baseRepository.GetByIdAndCode(saleValueDiscountItem.Id, saleValueDiscountItem.Code);
                if (exist != null)
                {
                    return new ServiceResponse<SaleValueDiscountItem>($"A Discount Item With the Provided Code and or Id Already Exist");
                }

                await _baseRepository.Create(saleValueDiscountItem);
                return new ServiceResponse<SaleValueDiscountItem>(saleValueDiscountItem);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<SaleValueDiscountItem>($"An Error Occured While Creating The Discount Item. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"SVD{random.ToUpper()}";
        }

        public async Task<ServiceResponse<SaleValueDiscountItem>> Update(Guid id, UpdateSaleValueDiscountItemRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<SaleValueDiscountItem>($"The requested Discount Item could not be found");
                }

                result.SaleValueDiscountId = request.SaleValueDiscountId;
                result.SaleValue = request.SaleValue;
                result.DiscountRate = request.DiscountRate;
                result.EffectiveDate = request.EffectiveDate;
                result.EndDate = request.EndDate;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<SaleValueDiscountItem>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<SaleValueDiscountItem>($"An Error Occured While Updating The Discount Item. {ex.Message}");
            }
        }
    }
}
