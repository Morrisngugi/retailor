using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PromotionDiscountItemRepository : BaseRepository<PromotionDiscountItem>, IPromotionDiscountItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PromotionDiscountItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}