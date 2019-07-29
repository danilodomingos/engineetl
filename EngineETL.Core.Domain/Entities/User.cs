using System;
using System.Collections.Generic;
using System.Text;

namespace EngineETL.Core.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime LastAccess { get; set; }
        public ICollection<Template> ExpectedFormats { get; set; }
    }
}
