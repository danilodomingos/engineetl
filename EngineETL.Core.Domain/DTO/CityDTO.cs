
using System.Collections.Generic;

namespace EngineETL.Core.Domain.DTO
{
    public class CityDTO
    {
        public string City { get; set; }
        public int Habitants { get; set; }
        public ICollection<NeighborhoodDTO> Neighborhoods { get; set; }
    }
}
