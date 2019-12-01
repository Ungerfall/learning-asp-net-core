using Microsoft.EntityFrameworkCore;
using NorthwindStore.Data.Models;
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

        public Categories GetCategoryById(int? categoryId)
        {
            return dbContext.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
        }

        public void UpdateCategory(Categories category)
        {
            dbContext.Attach(category);
            dbContext.Entry(category).State = EntityState.Modified;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
