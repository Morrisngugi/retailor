using Core.Models;
using System;
using Core.Models.Requests;
using Core.Services.Communications;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPricingService : IBaseService<Pricing>
    {
        Task<ServiceResponse<Pricing>> Create(AddPricingRequest request);
        Task<ServiceResponse<Pricing>> Update(Guid Id, UpdatePricingRequest request);
        string GenerateCode(int length);
    }
}
