using Microsoft.AspNetCore.Builder;

namespace NorthwindStore.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseImageCaching(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageCacheMiddleware>();
        }
    }
}
