using Microsoft.AspNetCore.Builder;

namespace EngineETL.API
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomResponseMiddleware>();
        }
    }
}
