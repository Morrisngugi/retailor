using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICatalogRepository : IBaseRepository<Catalog>
    {
        Task<List<Catalog>> FindByEntityId(Guid entityId);
    }
}
