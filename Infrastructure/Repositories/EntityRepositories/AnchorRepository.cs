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
    public class AnchorRepository : BaseEntityRepository<Anchor>, IAnchorRepository
    {
        private readonly EntityDbContext _dbContext;

        public AnchorRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Anchor> GetByIdentityIdId(Guid id)
        {
            return await _dbContext.Set<Anchor>()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Supplier>> GetSuppliers(Guid anchorId)
        {
            var result = await _dbContext.Set<Anchor>()
                .Include(f => f.AnchorSuppliers)
                .ThenInclude(c => c.Supplier)
                //.ThenInclude(x => x.Catalogs)
                .FirstOrDefaultAsync(r => r.Id == anchorId);
            var suppliers = result.AnchorSuppliers.Select(c => c.Supplier).ToList();
            return suppliers;
        }
    }
}
