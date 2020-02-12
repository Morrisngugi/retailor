using Core.Models.EntityModel;
using Core.Repositories.EntityRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EntityRepositories
{
    public class CountryRepository : BaseEntityRepository<Country>, ICountryRepository
    {
        private readonly EntityDbContext _dbContext;

        public CountryRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> GetRegions(Guid countryId)
        {
            var result = await _dbContext.Set<Country>()
               .Include(f => f.Regions)
               .FirstOrDefaultAsync(r => r.Id == countryId);
            var regions = result.Regions.ToList();
            return regions;
        }
    }
}
