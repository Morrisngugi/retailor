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
    public class BrandService : BaseService<Brand>, IBrandService
    {
        private readonly IBaseRepository<Brand> _baseRepository;

        public BrandService(IBaseRepository<Brand> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<ServiceResponse<Brand>> Create(AddBrandRequest request)
        {
            try
            {
                var brand = new Brand
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    Name = request.Name
                };

                var exist = await _baseRepository.GetByIdAndCode(brand.Id, brand.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Brand>($"A Brand With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(brand.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<Brand>($"A Brand With the Provided Name Already Exist");
                }

                await _baseRepository.Create(brand);
                return new ServiceResponse<Brand>(brand);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Brand>($"An Error Occured While Creating The Brand. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"BRAND{random.ToUpper()}";
        }

        public async Task<ServiceResponse<Brand>> Update(Guid id, UpdateBrandRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Brand>($"The requested Brand could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Brand>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Brand>($"An Error Occured While Updating The Brand. {ex.Message}");
            }
        }
    }
}
