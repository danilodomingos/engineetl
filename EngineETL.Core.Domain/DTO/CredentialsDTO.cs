using System;

namespace EngineETL.Core.Domain.DTO
{
    public class CredentialsDTO
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Login { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
