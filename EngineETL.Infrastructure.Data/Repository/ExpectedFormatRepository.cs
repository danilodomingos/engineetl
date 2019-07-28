using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EngineETL.Infrastructure.Data.Repository
{
    public class ExpectedFormatRepository : BaseRepository<ExpectedFormat>, IExpectedFormatRepository
    {
        
        public ExpectedFormatRepository(EngineETLContext context): base(context)
        {
            
        }
    }
}
