
using System;
using EngineETL.Core.Domain.DTO;
using EngineETL.Core.Domain.Entities;
using EngineETL.Core.Domain.Interfaces.Repository;
using EngineETL.Core.Domain.Interfaces.Service;

namespace EngineETL.Core.Domain.Services
{
    public class TemplateService : BaseService<Template>, ITemplateService
    {
        private readonly ITemplateRepository repository;
        private readonly IUserService userService;

        public TemplateService(ITemplateRepository repository, IUserService userService) : base(repository)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public TemplateDTO Insert(InsertTemplateDTO dto, Guid userId)
        {
            var user = userService.GetById(userId);

            var template = new Template()
            {
                Name = dto.Name,
                PropertyCity = dto.PropertyCity,
                CityPropertyName = dto.CityPropertyName,
                CityPropertyHabitants = dto.CityPropertyHabitants,
                PropertyNeighborhood = dto.PropertyNeighborhood,
                NeighborhoodPropertyHabitants = dto.NeighborhoodPropertyHabitants,
                NeighborhoodPropertyName = dto.NeighborhoodPropertyName,
                User = user
            };

            repository.Insert(template);
            repository.SaveChanges();

            var templateDto = new TemplateDTO()
            {
                Id = template.Id.ToString(),
                Name = dto.Name,
                PropertyCity = dto.PropertyCity,
                CityPropertyName = dto.CityPropertyName,
                CityPropertyHabitants = dto.CityPropertyHabitants,
                PropertyNeighborhood = dto.PropertyNeighborhood,
                NeighborhoodPropertyHabitants = dto.NeighborhoodPropertyHabitants,
                NeighborhoodPropertyName = dto.NeighborhoodPropertyName,
            };

            return templateDto;
        }

        public TemplateDTO GetById(Guid id)
        {
            TemplateDTO dto = null;

            var template = repository.GetById(id);

            if(template != null)
            {
                dto = new TemplateDTO() {
                    PropertyCity = template.PropertyCity,
                    CityPropertyName = template.CityPropertyName,
                    CityPropertyHabitants = template.CityPropertyHabitants,
                    PropertyNeighborhood = template.PropertyNeighborhood,
                    NeighborhoodPropertyName = template.NeighborhoodPropertyName,
                    NeighborhoodPropertyHabitants = template.NeighborhoodPropertyHabitants
                };
            }

            return dto;
        }
    }
}
