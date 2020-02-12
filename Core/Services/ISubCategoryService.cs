using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISubCategoryService : IBaseService<SubCategory>
    {
        Task<ServiceResponse<SubCategory>> Create(AddSubCategoryRequest request);
        Task<ServiceResponse<SubCategory>> Update(Guid Id, UpdateSubCategoryRequest request);
        string GenerateCode(int length);
    }
}