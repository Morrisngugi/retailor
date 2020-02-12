using Core.Models.EntityModel;
using Core.Models.Requests.EntityRequest;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface IRegionService : IBaseService<Region>
    {
        Task<ServiceResponse<List<Address>>> GetAddresses(Guid regionId);
        Task<ServiceResponse<Region>> Update(Guid id, UpdateRegionRequest request);
        Task<ServiceResponse<Region>> Create(AddRegionRequest  request);
    }
}
