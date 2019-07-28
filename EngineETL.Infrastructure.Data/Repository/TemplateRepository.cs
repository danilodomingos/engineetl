using System;
using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EngineETL.Infrastructure.Data.Repository
{
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        
        public TemplateRepository(EngineETLContext context): base(context)
        {
            
        }
    }
}
