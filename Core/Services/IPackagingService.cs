using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPackagingService : IBaseService<Packaging>
    {
        Task<ServiceResponse<Packaging>> Update(Guid Id, UpdatePackagingRequest request);
        Task<ServiceResponse<Packaging>> Create(AddPackagingRequest request);
        string GenerateCode(int length);
    }
}
