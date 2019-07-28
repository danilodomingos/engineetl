
using System;

namespace EngineETL.Core.Domain.Entities
{
    public class Template : BaseEntity
    {
        public string Name { get; set; }

        public string PropertyCity { get; set; }
        public string CityPropertyName { get; set; }
        public string CityPropertyHabitants { get; set; }
        public string PropertyNeighborhood { get; set; }
        public string NeighborhoodPropertyName { get; set; }
        public string NeighborhoodPropertyHabitants { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
