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
    public class UnitOfMeasureService : BaseService<UnitOfMeasure>, IUnitOfMeasureService
    {
        private readonly IBaseRepository<UnitOfMeasure> _baseRepository;
        private readonly IUnitOfMeasureTypeService _uomTypeService;

        public UnitOfMeasureService(IUnitOfMeasureTypeService uomTypeService, IBaseRepository<UnitOfMeasure> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _uomTypeService = uomTypeService;
        }

        public async Task<ServiceResponse<UnitOfMeasure>> Create(AddUnitOfMeasureRequest request)
        {
            try
            {
                var uomType = await _uomTypeService.FindByIdInclusive(request.UnitOfMeasureTypeId, x => x.Include(p => p.UnitOfMeasures));
                if (!uomType.Success)
                {
                    return new ServiceResponse<UnitOfMeasure>($"The unit of measure type does not exist");
                }

                if (uomType.Data.UnitOfMeasures.Count > 0 && uomType.Data.UnitOfMeasures.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
                {
                    return new ServiceResponse<UnitOfMeasure>($"The unit of measure already exist exist");
                }

                var unitOfMeasure= new UnitOfMeasure
                {
                    Name = request.Name,
                    Code = GenerateCode(8),
                    Description = request.Description,
                    UnitOfMeasureTypeId = uomType.Data.Id,
                    Grammage = request.Grammage,
                    PackSize = request.PackSize,
                    PalletSize = request.PalletSize
                };
                var exist = await _baseRepository.GetByIdAndCode(unitOfMeasure.Id, unitOfMeasure.Code);
                if (exist != null)
                {
                    return new ServiceResponse<UnitOfMeasure>($"A Unit Of Measure With the Provided Code and or Id Already Exist");
                }
                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(unitOfMeasure.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<UnitOfMeasure>($"A Unit of Measure With the Provided Name Already Exist");
                }

                //uomType.Data.UnitOfMeasures.Add(unitOfMeasure);

                await _baseRepository.Create(unitOfMeasure);
                return new ServiceResponse<UnitOfMeasure>(unitOfMeasure);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UnitOfMeasure>($"An Error Occured While Creating The Unit Of Measure. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"UOM{random.ToUpper()}";
        }

        public async Task<ServiceResponse<UnitOfMeasure>> Update(Guid id, UpdateUnitOfMeasureRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<UnitOfMeasure>($"The requested Unit Of Measure Type could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;
                result.Grammage = request.Grammage;
                result.PackSize = request.PackSize;
                result.PalletSize = request.PalletSize;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<UnitOfMeasure>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<UnitOfMeasure>($"An Error Occured While Updating The Unit Of Measure Type. {ex.Message}");
            }
        }
    }

}
