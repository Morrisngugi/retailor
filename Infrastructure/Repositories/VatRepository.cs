using Core.Models;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class VatRepository : BaseRepository<Vat>, IVatRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VatRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
