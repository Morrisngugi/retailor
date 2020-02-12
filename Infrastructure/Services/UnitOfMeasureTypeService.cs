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
    public class UnitOfMeasureTypeService : BaseService<UnitOfMeasureType>, IUnitOfMeasureTypeService
    {
        private readonly IBaseRepository<UnitOfMeasureType> _baseRepository;

        public UnitOfMeasureTypeService(IBaseRepository<UnitOfMeasureType> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<UnitOfMeasureType>> Create(AddUnitOfMeasureTypeRequest request)
        {
            try
            {
                var unitOfMeasureType = new UnitOfMeasureType
                {
                    Name = request.Name,
                    Code = GenerateCode(8),
                    Description = request.Description
                };
                var exist = await _baseRepository.GetByIdAndCode(unitOfMeasureType.Id, unitOfMeasureType.Code);
                if (exist != null)
                {
                    return new ServiceResponse<UnitOfMeasureType>($"A Unit Of Measure Type With the Provided Code and or Id Already Exist");
                }
                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(unitOfMeasureType.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<UnitOfMeasureType>($"A Tier With the Provided Name Already Exist");
                }

                await _baseRepository.Create(unitOfMeasureType);
                return new ServiceResponse<UnitOfMeasureType>(unitOfMeasureType);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UnitOfMeasureType>($"An Error Occured While Creating The Unit Of Measure Type. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"UOMT{random.ToUpper()}";
        }

        public async Task<ServiceResponse<UnitOfMeasureType>> Update(Guid id, UpdateUnitOfMeasureTypeRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<UnitOfMeasureType>($"The requested Unit Of Measure Type could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<UnitOfMeasureType>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<UnitOfMeasureType>($"An Error Occured While Updating The Unit Of Measure Type. {ex.Message}");
            }
        }
    }
}
