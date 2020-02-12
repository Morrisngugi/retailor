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
    public class ConsumerRepository : BaseEntityRepository<Consumer>, IConsumerRepository
    {
        private readonly EntityDbContext _dbContext;

        public ConsumerRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Consumer> GetByIdentityIdId(Guid id)
        {
            return await _dbContext.Set<Consumer>()
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Merchant>> GetMerchants(Guid consumerId)
        {
            var result = await _dbContext.Set<Consumer>()
                .Include(f => f.ConsumerMerchants)
                .ThenInclude(c => c.Merchant)
                //.ThenInclude(x => x.Catalogs)
                .FirstOrDefaultAsync(r => r.Id == consumerId);
            var merchants = result.ConsumerMerchants.Select(c => c.Merchant).ToList();
            return merchants;
        }
    }
}
