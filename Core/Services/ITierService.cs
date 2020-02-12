using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITierService : IBaseService<Tier>
    {
        Task<ServiceResponse<Tier>> Update(Guid Id, UpdateTierRequest request);
        Task<ServiceResponse<Tier>> Create(AddTierRequest request);
        string GenerateCode(int length);
    }
}
