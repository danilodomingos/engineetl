using EngineETL.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineETL.Core.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByLogin(string login, string password);
        User GetTemplates(Guid userId);
    }
}
