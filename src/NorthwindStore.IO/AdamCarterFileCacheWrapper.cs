using System;
using System.IO;
using System.Runtime.Caching;
using System.Threading;

namespace NorthwindStore.IO
{
    public sealed class AdamCarterFileCacheWrapper : IFileCache
    {
        private static object @lock = new object();

        private readonly FileCache cache;

        public AdamCarterFileCacheWrapper()
        {
            cache = new FileCache(
                cacheRoot: Configuration.Dir,
                calculateCacheSize: false,
                cleanInterval: Configuration.ExpirationTime);
        }

        public MemoryStream GetOrCreate(string key, Func<MemoryStream> factory)
        {
            lock (@lock)
            {
                if (cache.Contains(key))
                {
                    var cachedBytes = cache[key] as byte[];
                    if (cachedBytes == null)
                        return null;

                    return new MemoryStream(cachedBytes);
                }
            }


            lock (@lock)
            {
                var cachedItems = cache.GetCount();
                if (cachedItems >= Configuration.MaxCachedCount)
                    return null;

                var memoryStream = factory.Invoke();
                cache.Add(
                    new CacheItem(key, memoryStream.ToArray()),
                    new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.Add(Configuration.ExpirationTime)
                    });

                return memoryStream;
            }
        }

        public FileCacheConfiguration Configuration { get; set; } = new FileCacheConfiguration
        {
            Dir = "/image-cache",
            ExpirationTime = Timeout.InfiniteTimeSpan,
            MaxCachedCount = FileCacheConfiguration.INFINITY_CACHE_ENTRIES
        };
    }
}
