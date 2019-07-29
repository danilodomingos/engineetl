using System.Collections.Generic;

namespace EngineETL.Core.Domain.DTO
{
    public class UserListTemplateDTO
    {
        public string Login { get; set; }
        public ICollection<TemplateDTO> Templates { get; set; }
    }
}
