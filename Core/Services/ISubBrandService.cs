using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISubBrandService : IBaseService<SubBrand>
    {
        Task<ServiceResponse<SubBrand>> Create(AddSubBrandRequest request);
        Task<ServiceResponse<SubBrand>> Update(Guid Id, UpdateSubBrandRequest request);
        string GenerateCode(int length);
    }
}