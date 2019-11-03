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

        [HttpGet("{id}.{format?}")]
        public IActionResult Get(int? id, string format)
        {
            if (format == null)
            {
                RedirectToAction(nameof(Index));
            }

            byte[] image = categoryRepository.GetCategoryById(id).AlignedPicture;
            return File(image, "image/bmp");
        }
    }
}
