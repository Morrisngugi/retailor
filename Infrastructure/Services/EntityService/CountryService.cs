using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
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
    public class CountryService : BaseEntityService<Country>, ICountryService
    {
        private readonly IBaseEntityRepository<Country> _baseEntityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICodeGeneratorService _codeGenerator;

        public CountryService(ICodeGeneratorService codeGenerator,ICountryRepository countryRepository, IBaseEntityRepository<Country> baseEntityRepository) : base(baseEntityRepository)
        {
            _baseEntityRepository = baseEntityRepository;
            _countryRepository = countryRepository;
            _codeGenerator = codeGenerator;
        }

        public async Task<ServiceResponse<List<Region>>> GetRegions(Guid countryId)
        {
            try
            {
                var result = await _countryRepository.GetRegions(countryId);
                if (result.Count > 0)
                {
                    return new ServiceResponse<List<Region>>(result);
                }
                else
                {
                    return new ServiceResponse<List<Region>>($"No Regions were found for the provided Country");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Region>>($"An Error Occured while fetching regions. {ex.Message}");
            }
        }


        public async Task<ServiceResponse<Country>> Create(AddCountryRequest request)
        {
            try
            {
                var checkExist = await _countryRepository.FindOneByConditions(c => c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
                if (checkExist != null)
                {
                    return new ServiceResponse<Country>($"A Country with name {request.Name} already exist.");
                }


                var country = new Country
                {
                    Code = $"CNTRY{_codeGenerator.GenerateRandomString(7)}",
                    Name = request.Name,
                    Description = request.Description
                };

                await _countryRepository.Create(country);
                return new ServiceResponse<Country>(country);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Country>($"An Error Occured While Creating a Country Resource. {ex.Message}");
            }
        }

        
        public async Task<ServiceResponse<Country>> Update(Guid id, UpdateCountryRequest request)
        {
            try
            {
                var country = await _countryRepository.GetById(id);
                if (country == null)
                {
                    return new ServiceResponse<Country>($"The Requested resource could not be found");
                }
                country.Name = request.Name;
                country.Description = request.Description;

                await _countryRepository.Update(country);
                return new ServiceResponse<Country>(country);
            }
            catch (Exception)
            {
                return new ServiceResponse<Country>($"An Error Occured while Updating Country Resource");
            }
        }
    }
}
