using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Core.Domain.Interfaces.Service;

namespace EngineETL.Core.Domain.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository): base(repository)
        {
            this.repository = repository;
        }
    }
}
