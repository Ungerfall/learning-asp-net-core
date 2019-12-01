using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NorthwindStore.Conventions;
using NorthwindStore.IO;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindStore.Middleware
{
    public class ImageCacheMiddleware
    {
        private const string DEFAULT_CONTENT_TYPE_IMAGE = "image/bmp";

        private readonly IFileCache fileCache;
        private readonly RequestDelegate next;
        private readonly ILogger<ImageCacheMiddleware> log;

        public ImageCacheMiddleware(IFileCache fileCache, RequestDelegate next, ILogger<ImageCacheMiddleware> log)
        {
            this.fileCache = fileCache ?? throw new ArgumentNullException(nameof(fileCache));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            PathString requestPath = context.Request.Path;
            var isImageRequest = ImageRequestPatterns.Collection.Any(x => x.IsMatch(requestPath));
            if (isImageRequest)
            {
                await using MemoryStream imageStream = fileCache.Get(requestPath);
                if (imageStream != null)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = DEFAULT_CONTENT_TYPE_IMAGE;
                    imageStream.Seek(0L, SeekOrigin.Begin);
                    await imageStream.CopyToAsync(context.Response.Body);
                    return;
                }
            }

            if (!isImageRequest)
            {
                await next(context);
                return;
            }

            // rewindable stream.
            // Source: https://stackoverflow.com/questions/43403941/how-to-read-asp-net-core-response-body
            var response = context.Response;
            var originalBody = response.Body;
            try
            {
                await using var memoryStream = new MemoryStream();
                response.Body = memoryStream;

                await next(context);

                memoryStream.Position = 0;
                fileCache.Create(requestPath, memoryStream);
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBody);
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
            }
            finally
            {
                response.Body = originalBody;
            }
        }
    }
}
