using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using Core.Services.EntityServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityService
{
    public class RegionService : BaseEntityService<Region>, IRegionService
    {
        private readonly IBaseEntityRepository<Region> _baseEntityRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ICodeGeneratorService _codeGenService;
        private readonly ICountryService _countryService;

        public RegionService(ICountryService countryService,ICodeGeneratorService codeGenService,IRegionRepository regionRepository, IBaseEntityRepository<Region> baseEntityRepository) :base(baseEntityRepository)
        {
            _baseEntityRepository = baseEntityRepository;
            _regionRepository = regionRepository;
            _codeGenService = codeGenService;
            _countryService = countryService;
        }

        public async Task<ServiceResponse<Region>> Create(AddRegionRequest request)
        {
            try
            {
                var country = await _countryService.FindByIdInclusive(request.CountryId, x => x.Include(p => p.Regions));
                if (!country.Success)
                {
                    return new ServiceResponse<Region>($"The country does not exist");
                }

                if (country.Data.Regions.Count > 0 && country.Data.Regions.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
                {
                    return new ServiceResponse<Region>($"The region with name {request.Name} already exist exist");
                }

                var region = new Region
                {
                    Code = $"RGN{_codeGenService.GenerateRandomString(8)}",
                    CountryId = request.CountryId,
                    Name = request.Name,
                    Description = request.Description
                };


                await _regionRepository.Create(region);
                return new ServiceResponse<Region>(region);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Region>($"An Error Occured while creating a region Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Region>> Update(Guid id, UpdateRegionRequest request)
        {
            try
            {
                var country = await _countryService.FindById(request.CountryId);
                if (!country.Success)
                {
                    return new ServiceResponse<Region>($"The provided Country was not found");
                }
                var region = await _regionRepository.GetById(id);
                region.Name = request.Name;
                region.CountryId = request.CountryId;
                region.Description = request.Description;

                await _regionRepository.Update(region);
                return new ServiceResponse<Region>(region);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Region>($"An Erroe Occured while updating Region Resource. {ex.Message}");
            }
        }


        public async Task<ServiceResponse<List<Address>>> GetAddresses(Guid regionId)
        {
            try
            {
                var result = await _regionRepository.GetAddresses(regionId);
                if (result.Count > 0)
                {
                    return new ServiceResponse<List<Address>>(result);
                }
                else
                {
                    return new ServiceResponse<List<Address>>($"No Addresses were found for the provided anchor");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Address>>($"An Error Occured while fetching Addresses. {ex.Message}");
            }
        }

    }
}
