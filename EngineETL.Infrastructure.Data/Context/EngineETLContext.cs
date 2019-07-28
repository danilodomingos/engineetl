
using EngineETL.Core.Domain.Entities;
using EngineETL.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EngineETL.Infrastructure.Data.Context
{
    public class EngineETLContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory;

        public EngineETLContext(DbContextOptions<EngineETLContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(loggerFactory);
            builder.EnableSensitiveDataLogging();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TemplateMap());
            builder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(builder);
        }

        public DbSet<Template> Templates { get; set; }

    }
}
