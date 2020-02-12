using Core.Models.EntityModel;
using Core.Repositories.EntityRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EntityRepositories
{
    public class ContactPersonRepository : BaseEntityRepository<ContactPerson>, IContactPersonRepository
    {
        private readonly EntityDbContext _dbContext;

        public ContactPersonRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseEntity> GetEntity(Guid contactPersonId)
        {
            var result = await _dbContext.Set<ContactPerson>()
               .Include(f => f.BaseEntity)
               .FirstOrDefaultAsync(r => r.Id == contactPersonId);

            return result.BaseEntity;
        }
    }
}
