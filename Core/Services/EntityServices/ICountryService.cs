using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface ICountryService : IBaseService<Country>
    {
        Task<ServiceResponse<List<Region>>> GetRegions(Guid countryId);
        Task<ServiceResponse<Country>> Update(Guid id, UpdateCountryRequest request);
        Task<ServiceResponse<Country>> Create(AddCountryRequest request);
    }
}

