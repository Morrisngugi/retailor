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
    public class CertainValueCertainProductDiscountService : BaseService<CertainValueCertainProductDiscount>, ICertainValueCertainProductDiscountService
    {
        private readonly IBaseRepository<CertainValueCertainProductDiscount> _baseRepository;

        public CertainValueCertainProductDiscountService(IBaseRepository<CertainValueCertainProductDiscount> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<CertainValueCertainProductDiscount>> Create(AddCertainValueCertainProductDiscount request)
        {
            try
            {
                var certainValueCertainProductDiscount = new CertainValueCertainProductDiscount
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    Name = request.Name
                };

                var exist = await _baseRepository.GetByIdAndCode(certainValueCertainProductDiscount.Id, certainValueCertainProductDiscount.Code);
                if (exist != null)
                {
                    return new ServiceResponse<CertainValueCertainProductDiscount>($"A Discount With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(certainValueCertainProductDiscount.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<CertainValueCertainProductDiscount>($"A Discount With the Provided Name Already Exist");
                }

                await _baseRepository.Create(certainValueCertainProductDiscount);
                return new ServiceResponse<CertainValueCertainProductDiscount>(certainValueCertainProductDiscount);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<CertainValueCertainProductDiscount>($"An Error Occured While Creating The Discount. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"CVCPD{random.ToUpper()}";
        }

        public async Task<ServiceResponse<CertainValueCertainProductDiscount>> Update(Guid id, UpdateCertainValueCertainProductDiscountRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<CertainValueCertainProductDiscount>($"The requested Discount could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<CertainValueCertainProductDiscount>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<CertainValueCertainProductDiscount>($"An Error Occured While Updating The Discount. {ex.Message}");
            }
        }
    }
}
