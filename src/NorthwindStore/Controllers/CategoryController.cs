using System;
using Microsoft.AspNetCore.Mvc;
using NorthwindStore.Data;

namespace NorthwindStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public IActionResult Index()
        {
            var categories = categoryRepository.GetCategories();
            return View(categories);
        }
    }
}
