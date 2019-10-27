using Microsoft.EntityFrameworkCore;
using NorthwindStore.Data.Models;
using System;
using System.Linq;

namespace NorthwindStore.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext dbContext;

        public ProductRepository(NorthwindContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Models.Products> GetProducts()
        {
            return dbContext.Products
                .Include(x => x.Supplier)
                .Include(x => x.Category);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
