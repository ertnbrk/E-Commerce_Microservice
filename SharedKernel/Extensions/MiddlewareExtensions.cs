using Microsoft.AspNetCore.Builder;

namespace SharedKernel.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SharedKernel.Middleware.ExceptionMiddleware>();
        }
    }
}

