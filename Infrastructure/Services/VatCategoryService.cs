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
    public class VatCategoryService : BaseService<VatCategory>, IVatCategoryService
    {
        private readonly IBaseRepository<VatCategory> _baseRepository;

        public VatCategoryService(IBaseRepository<VatCategory> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<VatCategory>> Create(AddVatCategoryRequest request)
        {
            try
            {
                var vatCategory = new VatCategory
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    Name = request.Name
                };

                var exist = await _baseRepository.GetByIdAndCode(vatCategory.Id, vatCategory.Code);
                if (exist != null)
                {
                    return new ServiceResponse<VatCategory>($"A Vat Category With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(vatCategory.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<VatCategory>($"A Va tCategory With the Provided Name Already Exist");
                }

                await _baseRepository.Create(vatCategory);
                return new ServiceResponse<VatCategory>(vatCategory);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<VatCategory>($"An Error Occured While Creating The Vat Category. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"VATCAT{random.ToUpper()}";
        }

        public async Task<ServiceResponse<VatCategory>> Update(Guid id, UpdateVatCategoryRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<VatCategory>($"The requested Brand could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<VatCategory>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<VatCategory>($"An Error Occured While Updating The Vat Category. {ex.Message}");
            }
        }
    }
}
