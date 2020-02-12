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
    public class PricingService : BaseService<Pricing>, IPricingService
    {
        private readonly IBaseRepository<Pricing> _baseRepository;

        public PricingService(IBaseRepository<Pricing> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<Pricing>> Create(AddPricingRequest request)
        {
            try
            {
                var pricing = new Pricing
                {
                    Code = GenerateCode(8),
                    Price = request.Price,
                    TierId = request.TierId,
                    ProductId = request.ProductId
                };

                var exist = await _baseRepository.GetByIdAndCode(pricing.Id, pricing.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Pricing>($"A Pricing With the Provided Code and or Id Already Exist");
                }

                await _baseRepository.Create(pricing);
                return new ServiceResponse<Pricing>(pricing);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Pricing>($"An Error Occured While Creating The Pricing. {ex.Message}");
            }
        }
        public async Task<ServiceResponse<Pricing>> Update(Guid id, UpdatePricingRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Pricing>($"The requested Pricing could not be found");
                }

                result.Price = request.Price;
                result.ProductId = request.ProductId;
                result.TierId = request.TierId;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Pricing>(result);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Pricing>($"An Error Occured While Updating The Pricing. {ex.Message}");
            }
        }
        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"PRC{random.ToUpper()}";
        }
    }
}
