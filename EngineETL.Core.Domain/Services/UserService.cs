using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Core.Domain.Interfaces.Service;
using System;
using System.Linq;

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

        public UserListTemplateDTO GetTemplates(Guid userId)
        {
            UserListTemplateDTO dto = null;

            User user = repository.GetTemplates(userId);

            if(user != null)
            {
                dto = new UserListTemplateDTO()
                {
                    Login = user.Login,
                    Templates = user.ExpectedFormats?.Select(x => new TemplateDTO()
                    {
                        Id = x.Id.ToString(),
                        Name = x.Name,
                        PropertyCity = x.PropertyCity,
                        CityPropertyHabitants = x.CityPropertyHabitants,
                        CityPropertyName = x.CityPropertyName,
                        PropertyNeighborhood = x.PropertyNeighborhood,
                        NeighborhoodPropertyHabitants = x.NeighborhoodPropertyHabitants,
                        NeighborhoodPropertyName = x.NeighborhoodPropertyName
                    }).ToList()
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
