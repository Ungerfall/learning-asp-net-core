using System.Linq;
using NorthwindStore.Data.Models;

namespace NorthwindStore.Data
{
    public interface ICategoryRepository
    {
        IQueryable<Models.Categories> GetCategories();
        Models.Categories GetCategoryById(int? categoryId);
        void UpdateCategory(Categories category);
        void Save();
    }
}
