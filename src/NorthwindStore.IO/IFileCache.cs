using System.IO;

namespace NorthwindStore.IO
{
    public interface IFileCache
    {
        MemoryStream Get(string key);
        void Create(string key, MemoryStream stream);
    }
}
