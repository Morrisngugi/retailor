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
    public class CertainValueCertainProductDiscountItemService : BaseService<CertainValueCertainProductDiscountItem>, ICertainValueCertainProductDiscountItemService
    {
        private readonly IBaseRepository<CertainValueCertainProductDiscountItem> _baseRepository;

        public CertainValueCertainProductDiscountItemService(IBaseRepository<CertainValueCertainProductDiscountItem> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<CertainValueCertainProductDiscountItem>> Create(AddCertainValueCertainProductDiscountItem request)
        {
            try
            {
                var certainValueCertainProductDiscountItem = new CertainValueCertainProductDiscountItem
                {
                    Code = GenerateCode(8),
                    Value = request.Value,
                    Quantity = request.Quantity,
                    EffectiveDate = request.EffectiveDate,
                    EndDate = request.EndDate,
                    ProductId = request.ProductId
                };

                var exist = await _baseRepository.GetByIdAndCode(certainValueCertainProductDiscountItem.Id, certainValueCertainProductDiscountItem.Code);
                if (exist != null)
                {
                    return new ServiceResponse<CertainValueCertainProductDiscountItem>($"A Discount Item With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Value.Equals(certainValueCertainProductDiscountItem.Value));
                if (exist2 != null)
                {
                    return new ServiceResponse<CertainValueCertainProductDiscountItem>($"A Discount Item With the Provided Value Already Exist");
                }

                await _baseRepository.Create(certainValueCertainProductDiscountItem);
                return new ServiceResponse<CertainValueCertainProductDiscountItem>(certainValueCertainProductDiscountItem);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<CertainValueCertainProductDiscountItem>($"An Error Occured While Creating The Discount Item. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"CVCPDI{random.ToUpper()}";
        }

        public async Task<ServiceResponse<CertainValueCertainProductDiscountItem>> Update(Guid id, UpdateCertainValueCertainProductDiscountItemRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<CertainValueCertainProductDiscountItem>($"The requested Discount Item could not be found");
                }

                request.ProductId = request.ProductId;
                result.Value = request.Value;
                result.Quantity = request.Quantity;
                result.EffectiveDate = request.EffectiveDate;
                result.EndDate = request.EndDate;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<CertainValueCertainProductDiscountItem>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<CertainValueCertainProductDiscountItem>($"An Error Occured While Updating The Discount Item. {ex.Message}");
            }
        }
    }
}
