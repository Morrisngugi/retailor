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
    public class SaleValueDiscountService : BaseService<SaleValueDiscount>, ISaleValueDiscountService
    {
        private readonly IBaseRepository<SaleValueDiscount> _baseRepository;

        public SaleValueDiscountService(IBaseRepository<SaleValueDiscount> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<SaleValueDiscount>> Create(AddSaleValueDiscountRequest request)
        {
            try
            {
                var saleValueDiscount = new SaleValueDiscount
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    TierId = request.TierId
                };

                var exist = await _baseRepository.GetByIdAndCode(saleValueDiscount.Id, saleValueDiscount.Code);
                if (exist != null)
                {
                    return new ServiceResponse<SaleValueDiscount>($"A Discount With the Provided Code and or Id Already Exist");
                }

                await _baseRepository.Create(saleValueDiscount);
                return new ServiceResponse<SaleValueDiscount>(saleValueDiscount);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<SaleValueDiscount>($"An Error Occured While Creating The Discount. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"SVDI{random.ToUpper()}";
        }

        public async Task<ServiceResponse<SaleValueDiscount>> Update(Guid id, UpdateSaleValueDiscountRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<SaleValueDiscount>($"The requested Discount could not be found");
                }

                result.TierId = request.TierId;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<SaleValueDiscount>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<SaleValueDiscount>($"An Error Occured While Updating The Discount. {ex.Message}");
            }
        }
    }
}
