using System;
using System.Linq;
using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace EngineETL.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public TEntity GetById(Guid id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            var list = context.ChangeTracker.Entries().Where(t => t.State == EntityState.Modified);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
