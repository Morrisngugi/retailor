using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IVatCategoryService : IBaseService<VatCategory>
    {
        Task<ServiceResponse<VatCategory>> Update(Guid Id, UpdateVatCategoryRequest request);
        Task<ServiceResponse<VatCategory>> Create(AddVatCategoryRequest request);
        string GenerateCode(int length);
    }
}
//        : IBaseService<VatCategory>
//    {
//        Task<ServiceResponse<VatCategory>> Update(Guid Id, UpdateVatCategoryRequest request);
//        Task<ServiceResponse<VatCategory>> Create(AddVatCategoryRequest request);
//        string GenerateCode(int length);
//    }
//}
