using Microsoft.AspNetCore.Mvc;
using NorthwindStore.Data;
using NorthwindStore.Filters;
using NorthwindStore.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NorthwindStore.Controllers
{
    [ServiceFilter(typeof(LoggingFilter))]
    [Authorize]
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

        [Route("images/{id}")]
        [Route("Category/{id}.{format?}")]
        [HttpGet]
        public IActionResult Get(int? id, string format)
        {
            if (format == null)
            {
                var requestPath = HttpContext.Request.Path.Value;
                if (!requestPath.StartsWith("/images/"))
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            var category = categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            byte[] image = category.AlignedPicture;
            return File(image, "image/bmp");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryRepository.GetCategoryById(id);
            var viewModel = new CategoryViewModel
            {
                CategoryModel = category,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await using var ms = new MemoryStream();
                await viewModel.CategoryImageFile.CopyToAsync(ms);
                if (ms.Length >= Conventions.FileUploading.BufferedMaxSize)
                {
                    ModelState.AddModelError("CategoryModel.Picture", "The file is too large");
                    return View(viewModel);
                }

                viewModel.CategoryModel.UploadPicture(ms.ToArray());
                categoryRepository.UpdateCategory(viewModel.CategoryModel);
                categoryRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}
