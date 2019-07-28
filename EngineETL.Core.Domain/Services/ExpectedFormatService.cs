
using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Core.Domain.Interfaces.Service;

namespace EngineETL.Core.Domain.Services
{
    public class ExpectedFormatService : BaseService<ExpectedFormat>, IExpectedFormatService
    {
        private readonly IExpectedFormatRepository repository;

        public ExpectedFormatService(IExpectedFormatRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
