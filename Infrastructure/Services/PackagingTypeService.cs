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
    public class PackagingTypeService : BaseService<PackagingType>, IPackagingTypeService
    {
        private readonly IBaseRepository<PackagingType> _baseRepository;
        private readonly IPackagingService _packagingTypeService;

        public PackagingTypeService(IPackagingService packagingTypeService, IBaseRepository<PackagingType> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _packagingTypeService = packagingTypeService;
        }

        public async Task<ServiceResponse<PackagingType>> Create(AddPackagingTypeRequest request)
        {
            try
            {
                var packaging = await _packagingTypeService.FindByIdInclusive(request.PackagingId, x => x.Include(p => p.PackagingTypes));
                if (!packaging.Success)
                {
                    return new ServiceResponse<PackagingType>($"The Packaging type does not exist");
                }

                if (packaging.Data.PackagingTypes.Count > 0 && packaging.Data.PackagingTypes.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
                {
                    return new ServiceResponse<PackagingType>($"The Packaging already exist exist");
                }

                var packagingType = new PackagingType
                {
                    Name = request.Name,
                    Code = GenerateCode(8),
                    Description = request.Description,
                    PackagingId = packaging.Data.Id
                };
                var exist = await _baseRepository.GetByIdAndCode(packagingType.Id, packagingType.Code);
                if (exist != null)
                {
                    return new ServiceResponse<PackagingType>($"A Packaging Type With the Provided Code and or Id Already Exist");
                }
                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(packagingType.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<PackagingType>($"A Packaging Type With the Provided Name Already Exist");
                }

                await _baseRepository.Create(packagingType);
                return new ServiceResponse<PackagingType>(packagingType);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<PackagingType>($"An Error Occured While Creating The Packaging Type. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"SBR{random.ToUpper()}";
        }

        public async Task<ServiceResponse<PackagingType>> Update(Guid id, UpdatePackagingTypeRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<PackagingType>($"The requested Packaging could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<PackagingType>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<PackagingType>($"An Error Occured While Updating The Packaging. {ex.Message}");
            }
        }
    }
}
