using Core.Models.EntityModel;
using Core.Repositories.EntityRepositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.EntityRepositories
{
    public class AddressRepository : BaseEntityRepository<Address>, IAddressRepository
    {
        private readonly EntityDbContext _dbContext;

        public AddressRepository(EntityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
