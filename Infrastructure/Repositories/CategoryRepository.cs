using Core.Models;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
   public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}