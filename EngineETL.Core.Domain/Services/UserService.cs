using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Core.Domain.Interfaces.Service;
using System;

namespace EngineETL.Core.Domain.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository): base(repository)
        {
            this.repository = repository;
        }

        public UserDTO GetByLogin(LoginCredentialsDTO login)
        {
            UserDTO dto = null;

            var user = repository.GetByLogin(login.Login, login.Password);

            if(user != null)
            {
                dto = new UserDTO()
                {
                    Login = user.Login,
                    LastAccess = user.LastAccess,
                    Id = user.Id.ToString()
                };
            }

            return dto;
            
        }

        public UserDTO Insert(InsertUserDTO userDTO)
        {
            var user = new User()
            {
                LastAccess = DateTime.Now,
                Login = userDTO.Login,
                Password = userDTO.Password
            };

            repository.Insert(user);
            repository.SaveChanges();

            var insertedDTO = new UserDTO()
            {
                Id = user.Id.ToString(),
                LastAccess = user.LastAccess,
                Login = user.Login
            };

            return insertedDTO;
        }
    }
}
