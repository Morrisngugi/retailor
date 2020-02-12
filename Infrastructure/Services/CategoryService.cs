using Core.Models;
using Core.Models.Requests;
using Core.Repositories;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
   public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IBaseRepository<Category> _baseRepository;

        public CategoryService(IBaseRepository<Category> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ServiceResponse<Category>> Create(AddCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    Code = GenerateCode(8),
                    Description = request.Description,
                    Name = request.Name
                };

                var exist = await _baseRepository.GetByIdAndCode(category.Id, category.Code);
                if (exist != null)
                {
                    return new ServiceResponse<Category>($"A Category With the Provided Code and or Id Already Exist");
                }

                var exist2 = await _baseRepository.FindOneByConditions(x => x.Name.ToLower().Equals(category.Name.ToLower()));
                if (exist2 != null)
                {
                    return new ServiceResponse<Category>($"A Category With the Provided Name Already Exist");
                }

                await _baseRepository.Create(category);
                return new ServiceResponse<Category>(category);

            }
            catch (Exception ex)
            {

                return new ServiceResponse<Category>($"An Error Occured While Creating The Category. {ex.Message}");
            }
        }

        public string GenerateCode(int length)
        {
            var random = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

            return $"CAT{random.ToUpper()}";
        }

        public async Task<ServiceResponse<Category>> Update(Guid id, UpdateCategoryRequest request)
        {
            try
            {
                var result = await _baseRepository.GetById(id);
                if (result == null)
                {
                    return new ServiceResponse<Category>($"The requested Category could not be found");
                }

                result.Name = request.Name;
                result.Description = request.Description;
                result.LastUpdated = DateTime.Now;

                await _baseRepository.Update(id, result);
                return new ServiceResponse<Category>(result);

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Category>($"An Error Occured While Updating The Category. {ex.Message}");
            }
        }
    }
}
