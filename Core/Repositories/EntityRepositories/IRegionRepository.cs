using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface IRegionRepository : IBaseEntityRepository<Region>
    {
        Task<List<Address>> GetAddresses(Guid regionId);
    }
}
