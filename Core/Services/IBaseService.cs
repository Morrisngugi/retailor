using Core.Models.EntityModels;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBaseService<TEntity> where TEntity : class, IEntity
    {
        Task<ServiceResponse<TEntity>> FindById(Guid entityId);
        Task<ServiceResponse<TEntity>> FindByIdInclusive(Guid entityId, Func<IQueryable<TEntity>, IQueryable<TEntity>> func);
        Task<ServiceResponse<TEntity>> FindByIdentityId(Guid entityId);
        Task<ServiceResponse<TEntity>> FindByCode(string entityCode);
        Task<ServiceResponse<List<TEntity>>> FindAll();
        Task<ServiceResponse<List<TEntity>>> FindAllInclusive(Func<IQueryable<TEntity>, IQueryable<TEntity>> func);
        Task<ServiceResponse<TEntity>> Delete(Guid entityId);
    }
}
