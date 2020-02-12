using Core.Models;
using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface ISupplierRepository : IBaseEntityRepository<Supplier>
    {
        Task<Supplier> GetByIdentityIdId(Guid id);
        Task<List<Merchant>> GetMerchants(Guid supplierId);
        //Task<List<Catalog>> GetCatalogs(Guid supplierId);
    }
}
