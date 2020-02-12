using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PromotionDiscountRepository : BaseRepository<PromotionDiscount>, IPromotionDiscountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PromotionDiscountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}