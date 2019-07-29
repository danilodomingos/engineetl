using System;
using System.Collections.Generic;
using System.Text;

namespace EngineETL.Core.Domain.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
