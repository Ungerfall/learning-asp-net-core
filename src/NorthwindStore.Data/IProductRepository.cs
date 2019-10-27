using System;
using System.Linq;

namespace NorthwindStore.Data
{
    public interface IProductRepository : IDisposable
    {
        IQueryable<Models.Products> GetProducts();
    }
}
