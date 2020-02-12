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
    public class PackagingService : BaseService<Packaging>, IPackagingService
    {
        private readonly IBaseRepository<Packaging> _baseRepository;

        public PackagingService(IBaseRepository<Packaging> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<Packaging>> Create(AddPackagingRequest request)
        {
            try
            {
                var packaging = new Packaging
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    Name = request.Name
                };

                var exist = await _baseRepository.GetByIdAndCode(packaging.Id, packaging.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Packaging>($"A Packaging With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(packaging.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<Packaging>($"A Packaging With the Provided Name Already Exist");
                }

                await _baseRepository.Create(packaging);
                return new ServiceResponse<Packaging>(packaging);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Packaging>($"An Error Occured While Creating The Packaging. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"PACK{random.ToUpper()}";
        }

        public async Task<ServiceResponse<Packaging>> Update(Guid id, UpdatePackagingRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Packaging>($"The requested Brand could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Packaging>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Packaging>($"An Error Occured While Updating The Brand. {ex.Message}");
            }
        }
    }
}
