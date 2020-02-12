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
    public class MerchantRepository : BaseEntityRepository<Merchant>, IMerchantRepository
    {
        private readonly EntityDbContext _dbContext;

        public MerchantRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Merchant> GetByIdentityIdId(Guid id)
        {
            return await _dbContext.Set<Merchant>()
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        //public async Task<List<Catalog>> GetCatalogs(Guid merchantId)
        //{
        //    var result = await _dbContext.Set<Merchant>()
        //        .Include(f => f.Catalogs)
        //        .FirstOrDefaultAsync(r => r.Id == merchantId);
        //    var catalogs = result.Catalogs.ToList();

        //    return catalogs;
        //}

        public async Task<List<Consumer>> GetConsumers(Guid merchantId)
        {
            var result = await _dbContext.Set<Merchant>()
                .Include(f => f.ConsumerMerchants)
                .ThenInclude(c => c.Consumer)
                .FirstOrDefaultAsync(r => r.Id == merchantId);
            var consumers = result.ConsumerMerchants.Select(c => c.Consumer).ToList();
            return consumers;
        }

        //public async Task<List<Catalog>> GetSupplierCatalogs(Guid merchantId)
        //{
            
        //    var result = await _dbContext.Set<Merchant>()
        //        .Include(f => f.MerchantSuppliers)
        //        .ThenInclude(c => c.Supplier)
        //        .ThenInclude(x => x.Catalogs)
        //        .FirstOrDefaultAsync(r => r.Id == merchantId);
        //    var supplierCatalogs = result.MerchantSuppliers.SelectMany(c => c.Supplier.Catalogs).ToList();
            
        //    return supplierCatalogs;
        //}

        public async Task<List<Supplier>> GetSuppliers(Guid merchantId)
        {
            var result = await _dbContext.Set<Merchant>()
                .Include(f => f.MerchantSuppliers)
                .ThenInclude(c => c.Supplier)
                .FirstOrDefaultAsync(r => r.Id == merchantId);
            var suppliers = result.MerchantSuppliers.Select(c => c.Supplier).ToList();
            return suppliers;
        }
    }
}
