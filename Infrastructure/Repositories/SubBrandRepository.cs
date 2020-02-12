using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class SubBrandRepository : BaseRepository<SubBrand>, ISubBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubBrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
