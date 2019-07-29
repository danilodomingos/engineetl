using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EngineETL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly TokenGenerator tokenGenerator;

        public AuthenticationController(IUserService userService, TokenGenerator tokenGenerator)
        {
            this.userService = userService;
            this.tokenGenerator = tokenGenerator;
        }


        [HttpPost]
        [Route("GetToken")]
        public CredentialsDTO GetToken([FromBody]LoginCredentialsDTO login)
        {
            CredentialsDTO dto = null;

            var user = userService.GetByLogin(login);

            if(user != null)
            {
                dto = new CredentialsDTO()
                {
                    UserId = user.Id.ToString(),
                    LastAccess = user.LastAccess,
                    Login = user.Login,
                    Token = tokenGenerator.Generate(Guid.Parse(user.Id))
                };
            }
            return dto;
        }

        [HttpPost]
        [Route("CreateUser")]
        public UserDTO Insert([FromBody] InsertUserDTO insertUserDTO)
        {
            return userService.Insert(insertUserDTO);
        }
    }
}
