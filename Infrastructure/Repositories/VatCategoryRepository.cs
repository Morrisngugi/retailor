using Core.Models;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class VatCategoryRepository : BaseRepository<VatCategory>, IVatCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VatCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}