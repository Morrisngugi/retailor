using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class SaleValueDiscountItemRepository : BaseRepository<SaleValueDiscountItem>, ISaleValueDiscountItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleValueDiscountItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}