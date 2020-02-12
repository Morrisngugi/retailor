using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CatalogRepository : BaseRepository<Catalog>, ICatalogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Catalog>> FindByEntityId(Guid entityId)
        {
            return await _dbContext.Set<Catalog>().AsNoTracking().ToListAsync();
        }
    }
}
