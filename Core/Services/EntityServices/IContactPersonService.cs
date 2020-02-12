using Core.Models.EntityModel;
using Core.Services.Communications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.EntityServices
{
    public interface IContactPersonService : IBaseService<ContactPerson>
    {
        Task<ServiceResponse<BaseEntity>> GetEntity(Guid contactPersonId);
    }
}
