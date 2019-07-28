using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EngineETL.Infrastructure.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        
        public UserRepository(EngineETLContext context): base(context)
        {
            
        }

        public User GetByLogin(string login, string password)
        {
            return context.Set<User>().FirstOrDefault(x => x.Login == login && x.Password == password);
        }

        public User GetTemplates(Guid userId)
        {
            return context.Set<User>().Include(x => x.ExpectedFormats)
                .FirstOrDefault(x => x.Id == userId);
        }
    }
}
