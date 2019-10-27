using Microsoft.EntityFrameworkCore;
using NorthwindStore.Data.Models;
using System;
using System.Linq;
using NorthwindStore.Data.Filters;

namespace NorthwindStore.Data
{
    public class ProductRepository : IProductRepository
    {
        internal const int TAKE_ALL_PRODUCTS_VALUE = 0;

        private readonly NorthwindContext dbContext;
        private readonly ProductFilter cfg;

        public ProductRepository(NorthwindContext dbContext, ProductFilter filter)
        {
            if (filter.MaximumCount < 0) throw new ArgumentException(nameof(filter));

            this.dbContext = dbContext;
            cfg = filter;
        }

        public IQueryable<Models.Products> GetProducts()
        {
            var products = dbContext.Products
                .Include(x => x.Supplier)
                .Include(x => x.Category);

            return cfg.MaximumCount != TAKE_ALL_PRODUCTS_VALUE
                ? products.Take(cfg.MaximumCount)
                : products;
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
