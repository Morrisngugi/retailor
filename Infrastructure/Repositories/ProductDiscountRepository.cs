using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ProductDiscountRepository : BaseRepository<ProductDiscount>, IProductDiscountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductDiscountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
