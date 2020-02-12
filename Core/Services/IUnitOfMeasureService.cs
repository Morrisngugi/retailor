using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUnitOfMeasureService : IBaseService<UnitOfMeasure>
    {
        Task<ServiceResponse<UnitOfMeasure>> Create(AddUnitOfMeasureRequest request);
        Task<ServiceResponse<UnitOfMeasure>> Update(Guid Id, UpdateUnitOfMeasureRequest request);
        string GenerateCode(int length);
    }
}
