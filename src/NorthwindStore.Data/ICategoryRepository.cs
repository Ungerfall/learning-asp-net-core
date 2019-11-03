using System.Linq;

namespace NorthwindStore.Data
{
    public interface ICategoryRepository
    {
        IQueryable<Models.Categories> GetCategories();
        Models.Categories GetCategoryById(int? categoryId);
    }
}
