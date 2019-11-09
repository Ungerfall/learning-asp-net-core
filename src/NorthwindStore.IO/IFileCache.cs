using System;
using System.IO;

namespace NorthwindStore.IO
{
    public interface IFileCache
    {
        MemoryStream GetOrCreate(string key, Func<MemoryStream> factory);
    }
}
