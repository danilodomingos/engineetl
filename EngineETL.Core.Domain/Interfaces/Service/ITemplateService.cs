using System;
using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Entities;

namespace EngineETL.Core.Domain.Interfaces.Service
{
    public interface ITemplateService : IBaseService<Template>
    {
        TemplateDTO Insert(InsertTemplateDTO templateDTO, Guid userId);
        TemplateDTO GetById(Guid id);
    }
}
