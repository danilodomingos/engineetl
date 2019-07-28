using System;

namespace EngineETL.Core.Domain.DTO
{
    public class CredentialsDTO
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
