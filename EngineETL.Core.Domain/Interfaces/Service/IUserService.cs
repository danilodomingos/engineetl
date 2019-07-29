using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Entities;
using System;

namespace EngineETL.Core.Domain.Interfaces.Service
{
    public interface IUserService: IBaseService<User>
    {
        UserDTO GetByLogin(LoginCredentialsDTO login);
        UserDTO Insert(InsertUserDTO userDTO);
        UserListTemplateDTO GetTemplates(Guid userId);
    }
}
