using System;
using System.Collections.Generic;
using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Interfaces.Service;
using EngineETL.Tools.Parsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EngineETL.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {

        private readonly ITemplateService service;
        private readonly IUserService userService;

        public TemplateController(ITemplateService service, IUserService userService)
        {
            this.service = service;
            this.userService = userService;
        }

        [HttpGet]
        [Route("ListTemplates/{userId}")]
        public UserListTemplateDTO ListTemplates([FromRoute] Guid userId)
        {
            return userService.GetTemplates(userId);
        }

        [HttpPost]
        [Route("SaveTemplate/{userId}")]
        [Consumes("application/json")]
        public TemplateDTO SaveTemplate([FromBody] InsertTemplateDTO templateDTO, [FromRoute] Guid userId)
        {
            return service.Insert(templateDTO, userId);
        }


        [HttpPost]
        [Route("ConvertMessage/{templateId}")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/json")]
        public IList<CityDTO> ConvertMessage([FromBody]object data, [FromRoute] Guid templateId)
        {
            var expectedFormat = service.GetById(templateId);

            var parser = new InputToCityDTO();
            var cities = parser.ParseToList(data, expectedFormat);

            return cities;

        }

        
    }
}
