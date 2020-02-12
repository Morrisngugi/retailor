using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IVatService : IBaseService<Vat>
    {
        Task<ServiceResponse<Vat>> Create(AddVatRequest request);
        Task<ServiceResponse<Vat>> Update(Guid Id, UpdateVatRequest request);
        string GenerateCode(int length);
    }
}