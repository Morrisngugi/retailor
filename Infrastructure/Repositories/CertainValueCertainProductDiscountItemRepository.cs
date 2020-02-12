using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CertainValueCertainProductDiscountItemRepository : BaseRepository<CertainValueCertainProductDiscountItem>, ICertainValueCertainProductDiscountItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CertainValueCertainProductDiscountItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}