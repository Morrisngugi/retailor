using Core.Models;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PackagingRepository : BaseRepository<Packaging>, IPackagingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PackagingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
