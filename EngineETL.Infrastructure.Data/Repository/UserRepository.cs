using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EngineETL.Infrastructure.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        
        public UserRepository(EngineETLContext context): base(context)
        {
            
        }
    }
}
