using Core.Models.EntityModels;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BaseEntityService<TEntity> : IBaseService<TEntity> where TEntity : class, IEntity
    {
        private readonly IBaseEntityRepository<TEntity> _baseEntityRepository;

        public BaseEntityService(IBaseEntityRepository<TEntity> baseEntityRepository)
        {
            _baseEntityRepository = baseEntityRepository;
        }

        public async Task<ServiceResponse<TEntity>> Delete(Guid entityId)
        {
            try
            {
                var entityResouce = await _baseEntityRepository.GetById(entityId);

                if (entityResouce == null)
                {
                    return new ServiceResponse<TEntity>($"The Resource Could not be Found");
                }
                else
                {
                    await _baseEntityRepository.Delete(entityId);
                    return new ServiceResponse<TEntity>(entityResouce);
                }

            }
            catch (Exception ex)
            {

                return new ServiceResponse<TEntity>($"An Error Occured While deleting the Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<TEntity>>> FindAll()
        {
            List<TEntity> factories = new List<TEntity>();
            try
            {

                var entityResouce = await _baseEntityRepository.FindAll();

                if (entityResouce.Count <= 0)
                {
                    return new ServiceResponse<List<TEntity>>($"No Entities Found");
                }

                foreach (var item in entityResouce)
                {
                    factories.Add(item);
                }
                return new ServiceResponse<List<TEntity>>(factories);
            }
            catch (Exception ex)
            {

                return new ServiceResponse<List<TEntity>>($"An Error Occured While fetching the requested Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TEntity>> FindByCode(string entityCode)
        {
            try
            {
                var entityResouce = await _baseEntityRepository.GetByCode(entityCode);

                if (entityResouce == null)
                {
                    return new ServiceResponse<TEntity>($"The Requested Resource Could not be Found");
                }
                return new ServiceResponse<TEntity>(entityResouce);
            }
            catch (Exception ex)
            {

                return new ServiceResponse<TEntity>($"An Error Occured While fetching the requested Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TEntity>> FindById(Guid entityId)
        {
            try
            {
                var entityResouce = await _baseEntityRepository.GetById(entityId);

                if (entityResouce == null)
                {
                    return new ServiceResponse<TEntity>($"The Requested Resource Could not be Found");
                }
                return new ServiceResponse<TEntity>(entityResouce);
            }
            catch (Exception ex)
            {

                return new ServiceResponse<TEntity>($"An Error Occured While fetching the requested Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TEntity>> FindByIdInclusive(Guid entityId, Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            try
            {
                var entityResouce = await _baseEntityRepository.GetByIdInclusive(entityId, func);

                if (entityResouce == null)
                {
                    return new ServiceResponse<TEntity>($"The Requested Resource Could not be Found");
                }
                return new ServiceResponse<TEntity>(entityResouce);
            }
            catch (Exception ex)
            {

                return new ServiceResponse<TEntity>($"An Error Occured While fetching the requested Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TEntity>> FindByIdentityId(Guid entityId)
        {
            try
            {
                var entityResouce = await _baseEntityRepository.GetByIdentityId(entityId);

                if (entityResouce == null)
                {
                    return new ServiceResponse<TEntity>($"The Requested Resource Could not be Found");
                }
                return new ServiceResponse<TEntity>(entityResouce);
            }
            catch (Exception ex)
            {

                return new ServiceResponse<TEntity>($"An Error Occured While fetching the requested Resource. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<TEntity>>> FindAllInclusive(Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {

                var entityResouce = await _baseEntityRepository.FindAllInclusive(func);

                if (entityResouce.Count <= 0)
                {
                    return new ServiceResponse<List<TEntity>>($"No Entities Found");
                }

                foreach (var item in entityResouce)
                {
                    entities.Add(item);
                }
                return new ServiceResponse<List<TEntity>>(entities);
            }
            catch (Exception ex)
            {

                return new ServiceResponse<List<TEntity>>($"An Error Occured While fetching the requested Resource. {ex.Message}");
            }
        }
    }
}

