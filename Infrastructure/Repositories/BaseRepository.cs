using Core.Models.EntityModels;
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

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(List<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<List<TEntity>> FindAll()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetAllInclusive()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> GetByCode(string code)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Code == code);
        }

        public async Task<TEntity> GetByIdAndCode(Guid id, string code)
        {
            return await _dbContext.Set<TEntity>()
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id && e.Code == code);
        }

        public async Task Update(Guid id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> FindByConditions(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().Where(expression).AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public async Task<TEntity> FindOneByConditions(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> Count(TEntity entity)
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }

        public async Task<TEntity> GetByIdentityId(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            var result =  _dbContext.Set<TEntity>();

            IQueryable<TEntity> resultWithEagerLoading = func(result);

            return await resultWithEagerLoading.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<TEntity>> FindAllInclusive(Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            var result = _dbContext.Set<TEntity>();

            var resultWithEagerLoading = func(result);

            return await resultWithEagerLoading.ToListAsync();
        }
    }
}
