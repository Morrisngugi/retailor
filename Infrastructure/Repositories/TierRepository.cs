using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class TierRepository : BaseRepository<Tier>, ITierRepository
    {
        private readonly ApplicationDbContext _dbContext;
       
        public TierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
