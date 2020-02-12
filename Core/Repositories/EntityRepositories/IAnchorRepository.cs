using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface IAnchorRepository : IBaseEntityRepository<Anchor>
    {
        Task<Anchor> GetByIdentityIdId(Guid id);
        Task<List<Supplier>> GetSuppliers(Guid anchorId);
    }
}
