using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CertainValueCertainProductDiscountRepository : BaseRepository<CertainValueCertainProductDiscount>, ICertainValueCertainProductDiscountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CertainValueCertainProductDiscountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}