
using EngineETL.Core.Domain.Entities;
using EngineETL.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

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


            #region Mock In Database 

            builder.Entity<User>().HasData(new
            {
                Id = new Guid("04cc9f3e-7014-4bda-9af6-08d713670934"),
                Login = "usrteste@gmail.com",
                Password = "1234",
                LastAccess = DateTime.Now
            });

            builder.Entity<Template>().HasData(
                new 
                {
                    Id = new Guid("f342789f-8376-45c0-34ad-08d71388325b"),
                    Name = "# Exemplo 1 - Template Estado Rio de Janeiro",
                    PropertyCity = "corpo.cidade",
                    CityPropertyName = "cidade.nome",
                    CityPropertyHabitants = "cidade.populacao",
                    PropertyNeighborhood = "cidades.bairros.bairro",
                    NeighborhoodPropertyName = "bairro.nome",
                    NeighborhoodPropertyHabitants = "bairro.populacao",
                    UserId = new Guid("04cc9f3e-7014-4bda-9af6-08d713670934")
                },
                new
                {
                    Id = new Guid("5019b3d0-aa57-440c-b9cc-08d7138a0915"),
                    Name = "# Exemplo 2 - Template Estado Minas Gerais",
                    PropertyCity = "body.region.cities.city",
                    CityPropertyName = "city.name",
                    CityPropertyHabitants = "city.population",
                    PropertyNeighborhood = "city.neighborhoods.neighborhood",
                    NeighborhoodPropertyName = "neighborhood.name",
                    NeighborhoodPropertyHabitants = "neighborhood.population",
                    UserId = new Guid("04cc9f3e-7014-4bda-9af6-08d713670934")
                },
                new
                {
                    Id = new Guid("73896430-392a-4d76-b9cd-08d7138a0915"),
                    Name = "# Exemplo 3 - Template Estado do Acre",
                    PropertyCity = "cities",
                    CityPropertyName = "cities.name",
                    CityPropertyHabitants = "cities.population",
                    PropertyNeighborhood = "cities.neighborhoods",
                    NeighborhoodPropertyName = "neighborhoods.name",
                    NeighborhoodPropertyHabitants = "neighborhoods.population",
                    UserId = new Guid("04cc9f3e-7014-4bda-9af6-08d713670934")
                });

            #endregion

            base.OnModelCreating(builder);
        }

        public DbSet<Template> Templates { get; set; }

    }
}
