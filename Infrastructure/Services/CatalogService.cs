using Core.Models;
using Core.Models.EntityModel;
using Core.Models.Requests;
using Core.Repositories;
using Core.Services;
using Core.Services.Communications;
using Core.Services.EntityServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CatalogService : BaseService<Catalog>, ICatalogService
    {

        private readonly IBaseRepository<Catalog> _baseRepository;
        private readonly IAnchorService _anchorService;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public CatalogService(IAnchorService anchorService, ICodeGeneratorService codeGeneratorService, IBaseRepository<Catalog> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _codeGeneratorService = codeGeneratorService;
            _anchorService = anchorService;
        }


        public async Task<ServiceResponse<Catalog>> AddProducts(Guid catalogId, List<Guid> productIds)
        {

            try
            {
                

                var catalog = await _baseRepository.GetById(catalogId, x => x.Include(ct => ct.CatalogProducts).ThenInclude(p => p.Product));
                if (catalog == null)
                {
                    return new ServiceResponse<Catalog>($"Catalog does not Exist");
                }

                //todo implement add to catalog


                await _baseRepository.Update(catalog);
                return new ServiceResponse<Catalog>(catalog);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Catalog>($"An Error Occured While Products to the Catalog. {ex.Message}");
            }
        }

        public async Task<bool> IsExpired(Guid catalogId)
        {
            try
            {
                var result = await _baseRepository.GetById(catalogId);
                if (result == null)
                {
                    return true;
                }

                
                return result.EndDate >= DateTime.Now;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public async Task<ServiceResponse<Catalog>> RemoveProducts(Guid catalogId, List<Guid> productIds)
        {
            try
            {
                var catalog = await _baseRepository.GetById(catalogId, x => x.Include(ct => ct.CatalogProducts).ThenInclude(p => p.Product));
                if (catalog == null)
                {
                    return new ServiceResponse<Catalog>($"Catalog does not Exist");
                }

                //todo implement remove from catalog


                await _baseRepository.Update(catalog);
                return new ServiceResponse<Catalog>(catalog);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Catalog>($"An Error Occured While Products to the Catalog. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Catalog>> Create(AddCatalogRequest request)
        {

            try
            {
                var catalog = new Catalog
                {
                    Code = $"CTLG{_codeGeneratorService.GenerateRandomString(8)}",
                    Description = request.Description,
                    Name = request.Name,
                    EffectiveDate = request.EffectiveDate,
                    EndDate = request.EndDate,
                    EntityId = request.EntityId,
                    Published = request.Published
                };

                var exist = await _baseRepository.GetByIdAndCode(catalog.Id, catalog.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Catalog>($"A Catalog With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(catalog.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<Catalog>($"A Catalog With the Provided Name Already Exist");
                }

                await _baseRepository.Create(catalog);
                return new ServiceResponse<Catalog>(catalog);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Catalog>($"An Error Occured While Creating the Catalog. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<Catalog>> Update(Guid id, UpdateCatalogRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Catalog>($"The requested Catalog could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;
                result.EffectiveDate = request.EffectiveDate;
                result.EndDate = request.EndDate;
                result.EntityId = request.EntityId;
                result.Published = request.Published;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Catalog>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Catalog>($"An Error Occured While Updating The Catalog. {ex.Message}");
            }
        }

        public async Task<ServiceResponse<BaseEntity>> GetCatalogEntity(Guid entityId)
        {
            try
            {
                var result = await _anchorService.FindById(entityId);
                if (!result.Success)
                {
                    return new ServiceResponse<BaseEntity>($"The requested Catalog could not be found");
                }

                
                return new ServiceResponse<BaseEntity>(result.Data);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<BaseEntity>($"An Error Occured While fetching the catalog entity. {ex.Message}");
            }
        }
    }
}
