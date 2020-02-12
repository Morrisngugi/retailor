using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBrandService : IBaseService<Brand>
    {
        Task<ServiceResponse<Brand>> Update(Guid Id, UpdateBrandRequest request);
        Task<ServiceResponse<Brand>> Create(AddBrandRequest request);
        string GenerateCode(int length);
    }
}
