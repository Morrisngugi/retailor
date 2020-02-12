using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWorkRepository
    {
        public async Task CompleteAsync()
        {
           //await 
        }
    }
}
