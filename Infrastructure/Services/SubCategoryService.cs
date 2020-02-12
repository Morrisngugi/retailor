using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Services;
using Core.Services.Communications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SubCategoryService : BaseService<SubCategory>, ISubCategoryService
    {
        private readonly IBaseRepository<SubCategory> _baseRepository;
        private readonly ICategoryService _subCategoryService;

        public SubCategoryService(ICategoryService subCategoryService, IBaseRepository<SubCategory> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _subCategoryService = subCategoryService;
        }

        public async Task<ServiceResponse<SubCategory>> Create(AddSubCategoryRequest request)
        {
            try
            {
                var category = await _subCategoryService.FindByIdInclusive(request.CategoryId, x => x.Include(p => p.SubCategories));
                if (!category.Success)
                {
                    return new ServiceResponse<SubCategory>($"The Category does not exist");
                }

                if (category.Data.SubCategories.Count > 0 && category.Data.SubCategories.Any(x => x.Name.ToLower().Equals(request.Name.ToLower())))
                {
                    return new ServiceResponse<SubCategory>($"The Category already exist exist");
                }

                var subCategory = new SubCategory
                {
                    Name = request.Name,
                    Code = GenerateCode(8),
                    Description = request.Description,
                    CategoryId = category.Data.Id
                };
                var exist = await _baseRepository.GetByIdAndCode(subCategory.Id, subCategory.Code);
                if (exist != null)
                {
                    return new ServiceResponse<SubCategory>($"A Sub Category With the Provided Code and or Id Already Exist");
                }
                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(subCategory.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<SubCategory>($"A A Sub Category With the Provided Name Already Exist");
                }

                //uomType.Data.UnitOfMeasures.Add(unitOfMeasure);

                await _baseRepository.Create(subCategory);
                return new ServiceResponse<SubCategory>(subCategory);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<SubCategory>($"An Error Occured While Creating The Sub Category. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"SBC{random.ToUpper()}";
        }

        public async Task<ServiceResponse<SubCategory>> Update(Guid id, UpdateSubCategoryRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<SubCategory>($"The requested Category could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<SubCategory>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<SubCategory>($"An Error Occured While Updating The Category. {ex.Message}");
            }
        }
    }
}
