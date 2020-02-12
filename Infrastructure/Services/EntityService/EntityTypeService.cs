using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Repositories;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using Core.Services.EntityServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityService
{
    public class EntityTypeService : BaseEntityService<EntityType>, IEntityTypeService
    {
        private readonly IBaseEntityRepository<EntityType> _baseRepository;
        private readonly ICodeGeneratorService _codeGen;

        public EntityTypeService(ICodeGeneratorService codeGen, IBaseEntityRepository<EntityType> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _codeGen = codeGen;
        }

        public async Task<ServiceResponse<EntityType>> Create(AddEntityTypeRequest request)
        {
            try
            {
                var tier = new EntityType
                {
                    Code = $"ETTP{_codeGen.GenerateRandomString(8)}",
                    Description = request.Description,
                    Name = request.Name,
                };

                var exist = await _baseRepository.GetByIdAndCode(tier.Id, tier.Code);
                if (exist != null)
                {
                    return new ServiceResponse<EntityType>($"An Entity Type With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(tier.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<EntityType>($"An Entity Type With the Provided Name Already Exist");
                }

                await _baseRepository.Create(tier);
                return new ServiceResponse<EntityType>(tier);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<EntityType>($"An Error Occured While Creating The Entity Type. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<EntityType>> Update(Guid id, UpdateEntityTypeRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<EntityType>($"The requested Tier could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<EntityType>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<EntityType>($"An Error Occured While Updating The Entity Type. {ex.Message}");
            }
        }
    }
}
