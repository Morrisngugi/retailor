using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Services;
using Core.Services.Communications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SubBrandService : BaseService<SubBrand>, ISubBrandService
    {
        private readonly IBaseRepository<SubBrand> _baseRepository;
        private readonly IBrandService _subBrandService;

        public SubBrandService(IBrandService subBrandService, IBaseRepository<SubBrand> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _subBrandService = subBrandService;
        }

        public async Task<ServiceResponse<SubBrand>> Create(AddSubBrandRequest request)
        {
            try
            {
                var brand = await _subBrandService.FindByIdInclusive(request.BrandId, x => x.Include(p => p.SubBrands));
                if (!brand.Success)
                {
                    return new ServiceResponse<SubBrand>($"The unit of measure type does not exist");
                }

                if (brand.Data.SubBrands.Count > 0 && brand.Data.SubBrands.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
                {
                    return new ServiceResponse<SubBrand>($"The unit of measure already exist exist");
                }

                var subBrand = new SubBrand
                {
                    Name = request.Name,
                    Code = GenerateCode(8),
                    Description = request.Description,
                    BrandId = brand.Data.Id
                };
                var exist = await _baseRepository.GetByIdAndCode(subBrand.Id, subBrand.Code);
                if (exist != null)
                {
                    return new ServiceResponse<SubBrand>($"A Sub Brand With the Provided Code and or Id Already Exist");
                }
                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(subBrand.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<SubBrand>($"A Sub Brand With the Provided Name Already Exist");
                }

                //uomType.Data.UnitOfMeasures.Add(unitOfMeasure);

                await _baseRepository.Create(subBrand);
                return new ServiceResponse<SubBrand>(subBrand);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<SubBrand>($"An Error Occured While Creating The Sub Brand. {ex.Message}");
            }
        }
            


        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"SBR{random.ToUpper()}";
        }

        public async Task<ServiceResponse<SubBrand>> Update(Guid id, UpdateSubBrandRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<SubBrand>($"The requested Brand could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<SubBrand>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<SubBrand>($"An Error Occured While Updating The Brand. {ex.Message}");
            }
        }
    }
}
