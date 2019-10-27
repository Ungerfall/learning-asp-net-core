using System;
using System.Linq;

namespace NorthwindStore.Data
{
    public interface ICategoryRepository : IDisposable
    {
        IQueryable<Models.Categories> GetCategories();
    }
}
