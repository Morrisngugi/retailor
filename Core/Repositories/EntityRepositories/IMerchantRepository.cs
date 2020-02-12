using Core.Models;
using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface IMerchantRepository : IBaseEntityRepository<Merchant>
    {
        Task<Merchant> GetByIdentityIdId(Guid id);
        Task<List<Supplier>> GetSuppliers(Guid merchantId);
        Task<List<Consumer>> GetConsumers(Guid merchantId);
        //Task<List<Catalog>> GetCatalogs(Guid merchantId);
        //Task<List<Catalog>> GetSupplierCatalogs(Guid merchantId);
    }
}
