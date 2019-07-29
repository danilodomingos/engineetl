using System;

namespace EngineETL.Core.Domain.Common.Exceptions
{
    public class HandledException : Exception
    {
        public HandledException(string message) : base(message)
        {

        }
    }
}
