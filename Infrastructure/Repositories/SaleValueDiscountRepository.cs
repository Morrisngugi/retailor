using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class SaleValueDiscountRepository : BaseRepository<SaleValueDiscount>, ISaleValueDiscountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleValueDiscountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}