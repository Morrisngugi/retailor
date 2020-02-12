using Core.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface IBaseEntityRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> FindAll();
        IQueryable<TEntity> GetAllInclusive();

        Task<TEntity> GetById(Guid id);
        Task<TEntity> GetByIdInclusive(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> func);
        Task<TEntity> GetByIdentityId(Guid identityId);
        Task<TEntity> GetByCode(string code);

        Task<TEntity> GetByIdAndCode(Guid id, string code);

        Task Create(TEntity entity);

        Task Create(List<TEntity> entities);

        Task Update(Guid id, TEntity entity);
        Task Update(TEntity entity);

        Task Delete(Guid id);
        Task<int> Count(TEntity entity);


        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> FindByConditions(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FindOneByConditions(Expression<Func<TEntity, bool>> expression);

        //IQueryable<TEntity> FindAllInclusive(Func<IQueryable<TEntity>, IQueryable<TEntity>> func);
        Task<List<TEntity>> FindAllInclusive(Func<IQueryable<TEntity>, IQueryable<TEntity>> func);
        //Task<List<TEntity>> FindAllInclusive(Func<List<TEntity>, List<TEntity>> func);
    }
}
