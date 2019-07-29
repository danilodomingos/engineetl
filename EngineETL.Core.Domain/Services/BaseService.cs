
using System;
using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Core.Domain.Interfaces.Service;

namespace EngineETL.Core.Domain.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;

        }

        public void Delete(TEntity entity)
        {
            this.repository.Delete(entity);
        }

        public TEntity GetById(Guid id)
        {
            return this.repository.GetById(id);
        }

        public void Insert(TEntity entity)
        {
            this.repository.Insert(entity);
        }

        public int SaveChanges()
        {
            return this.repository.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            this.Update(entity);
        }
    }
}
