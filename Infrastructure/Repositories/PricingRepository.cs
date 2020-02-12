using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PricingRepository : BaseRepository<Pricing>, IPricingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PricingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}