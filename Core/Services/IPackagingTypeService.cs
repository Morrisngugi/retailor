using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPackagingTypeService : IBaseService<PackagingType>
    {
        Task<ServiceResponse<PackagingType>> Create(AddPackagingTypeRequest request);
        Task<ServiceResponse<PackagingType>> Update(Guid Id, UpdatePackagingTypeRequest request);
        string GenerateCode(int length);
    }
}