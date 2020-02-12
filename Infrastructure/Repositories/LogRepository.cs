using Core.Models;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LogRepository : ILogRepository<EwsLog>
    {
        private readonly LogDbContext _dbContext;

        public LogRepository(LogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<EwsLog>> FindAll()
        {
            return await _dbContext.Set<EwsLog>().AsNoTracking().OrderBy(x => x.TimeStamp).ToListAsync();
        }

        public async Task<List<EwsLog>> FindByConditions(Expression<Func<EwsLog, bool>> expression)
        {
            return await _dbContext.Set<EwsLog>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<EwsLog> GetById(int id)
        {
            return await _dbContext.Set<EwsLog>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
