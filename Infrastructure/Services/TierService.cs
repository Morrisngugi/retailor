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
    public class TierService : BaseService<Tier>, ITierService
    {

        private readonly IBaseRepository<Tier> _baseRepository;

        public TierService(IBaseRepository<Tier> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<Tier>> Create(AddTierRequest request)
        {
            try
            {
                var tier = new Tier
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    Name = request.Name
                };

                var exist = await _baseRepository.GetByIdAndCode(tier.Id, tier.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Tier>($"A Tier With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(tier.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<Tier>($"A Tier With the Provided Name Already Exist");
                }

                await _baseRepository.Create(tier);
                return new ServiceResponse<Tier>(tier);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Tier>($"An Error Occured While Creating The Tier. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"TIER{random.ToUpper()}";
        }

        public async Task<ServiceResponse<Tier>> Update(Guid id, UpdateTierRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Tier>($"The requested Tier could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Tier>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Tier>($"An Error Occured While Updating The Tier. {ex.Message}");
            }
        }
    }
}
