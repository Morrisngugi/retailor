using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using System;

namespace Infrastructure.Repositories
{
    public class UnitOfMeasureTypeRepository : BaseRepository<UnitOfMeasureType>, IUnitOfMeasureTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfMeasureTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
