using System;
using System.IO;
using System.Runtime.Caching;

namespace NorthwindStore.IO
{
    public sealed class AdamCarterFileCacheWrapper : IFileCache
    {
        private readonly FileCacheConfiguration configuration;
        private readonly FileCache cache;

        public AdamCarterFileCacheWrapper(FileCacheConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            cache = new FileCache(cacheRoot: configuration.Dir, calculateCacheSize: false);
        }

        public MemoryStream Get(string key)
        {
            if (!cache.Contains(key))
                return null;

            var cachedBytes = cache[key] as byte[];
            //race condition
            if (cachedBytes == null)
                return null;

            return new MemoryStream(cachedBytes);
        }

        public void Create(string key, MemoryStream stream)
        {
            var cachedItems = cache.GetCount();
            if (cachedItems >= configuration.MaxCachedCount)
                throw new Exception("Max items limit is exceeded");

            cache.Add(
                new CacheItem(key, stream.ToArray()),
                new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.Add(configuration.ExpirationTime)
                });
        }
    }
}
