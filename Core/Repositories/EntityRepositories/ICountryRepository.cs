using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface ICountryRepository : IBaseEntityRepository<Country>
    {
        Task<List<Region>> GetRegions(Guid countryId);
    }
}
