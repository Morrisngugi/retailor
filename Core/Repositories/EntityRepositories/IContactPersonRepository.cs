using Core.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.EntityRepositories
{
    public interface IContactPersonRepository : IBaseEntityRepository<ContactPerson>
    {
        Task<BaseEntity> GetEntity(Guid contactPersonId);
    }
}
