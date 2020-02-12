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
    public class RegionRepository : BaseEntityRepository<Region>, IRegionRepository
    {
        private readonly EntityDbContext _dbContext;

        public RegionRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Address>> GetAddresses(Guid regionId)
        {
            var result = await _dbContext.Set<Region>()
               .Include(f => f.Addresses)
               .FirstOrDefaultAsync(r => r.Id == regionId);
            var addresses = result.Addresses.ToList();
            return addresses;
        }
    }
}
