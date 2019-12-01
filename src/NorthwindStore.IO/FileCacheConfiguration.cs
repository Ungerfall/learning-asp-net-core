using System;

namespace NorthwindStore.IO
{
    public class FileCacheConfiguration
    {
        public const int INFINITY_CACHE_ENTRIES = -1;

        public string Dir { get; set; }
        public int MaxCachedCount { get; set; }
        public TimeSpan ExpirationTime { get; set; }
    }
}
