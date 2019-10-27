using NorthwindStore.Data.Models;
using System;
using System.Linq;

namespace NorthwindStore.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindContext dbContext;

        public CategoryRepository(NorthwindContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Categories> GetCategories()
        {
	        return dbContext.Categories;
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
