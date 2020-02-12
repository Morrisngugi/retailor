using Core.Models;
using Core.Models.Requests;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Core.Services
{
   public interface IUnitOfMeasureTypeService : IBaseService<UnitOfMeasureType>
    {
        Task<ServiceResponse<UnitOfMeasureType>> Create(AddUnitOfMeasureTypeRequest request);
        Task<ServiceResponse<UnitOfMeasureType>> Update(Guid Id, UpdateUnitOfMeasureTypeRequest request);
        string GenerateCode(int length);
    }
}
