using Core.Models;
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
    public class SupplierRepository : BaseEntityRepository<Supplier>, ISupplierRepository
    {
        private readonly EntityDbContext _dbContext;

        public SupplierRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Supplier> GetByIdentityIdId(Guid id)
        {
            return await _dbContext.Set<Supplier>()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        //public async Task<List<Catalog>> GetCatalogs(Guid supplierId)
        //{
        //    var result = await _dbContext.Set<Supplier>()
        //        .Include(f => f.Catalogs)
        //        .FirstOrDefaultAsync(r => r.Id == supplierId);
        //    var catalogs = result.Catalogs.ToList();

        //    return catalogs;
        //}

        public async Task<List<Merchant>> GetMerchants(Guid supplierId)
        {
            var result = await _dbContext.Set<Supplier>()
                .Include(f => f.MerchantSuppliers)
                .ThenInclude(c => c.Merchant)
                //.ThenInclude(x => x.Catalogs)
                .FirstOrDefaultAsync(r => r.Id == supplierId);
            var merchants = result.MerchantSuppliers.Select(c => c.Merchant).ToList();
            return merchants;
        }
    }
}
