using Core.Models.EntityModel;
using Core.Models.Requests;
using Core.Models.Requests.EntityRequest;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface IEntityTypeService : IBaseService<EntityType>
    {
        Task<ServiceResponse<EntityType>> Update(Guid Id, UpdateEntityTypeRequest request);
        Task<ServiceResponse<EntityType>> Create(AddEntityTypeRequest request);
    }
}
