using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ILogRepository<TEntity> where TEntity : class
    {

        Task<List<TEntity>> FindAll();
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> FindByConditions(Expression<Func<TEntity, bool>> expression);
    }
}
