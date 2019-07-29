using EngineETL.Core.Domain.Entities;
using System;

namespace EngineETL.Core.Domain.Interfaces.Service
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(Guid id);
        int SaveChanges();
    }
}
