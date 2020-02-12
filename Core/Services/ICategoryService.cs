using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
   public interface ICategoryService : IBaseService<Category>
    {
        Task<ServiceResponse<Category>> Create(AddCategoryRequest request);
        Task<ServiceResponse<Category>> Update(Guid Id, UpdateCategoryRequest request);
        string GenerateCode(int length);
    }
}
