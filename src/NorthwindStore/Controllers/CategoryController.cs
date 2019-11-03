using Microsoft.AspNetCore.Mvc;
using NorthwindStore.Data;
using NorthwindStore.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

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
