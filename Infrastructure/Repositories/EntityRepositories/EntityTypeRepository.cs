using Core.Models.EntityModel;
using Core.Repositories.EntityRepositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EntityRepositories
{
    public class EntityTypeRepository : BaseEntityRepository<EntityType>, IEntityTypeRepository
    {
        private readonly EntityDbContext _dbContext;

        public EntityTypeRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<EntityType> GetByIdInclusive(Guid id, Func<IQueryable<EntityType>, IQueryable<EntityType>> func)
        {
            throw new NotImplementedException();
        }
    }
}
