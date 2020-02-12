using Core.Models;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PackagingTypeRepository : BaseRepository<PackagingType>, IPackagingTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PackagingTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
