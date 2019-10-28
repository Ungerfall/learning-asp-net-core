using Microsoft.AspNetCore.Mvc;
using NorthwindStore.Data;
using NorthwindStore.ViewModels;
using System;

namespace NorthwindStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;

        public ProductController(
            ISupplierRepository supplierRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            this.supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public IActionResult Index()
        {
            var viewModel = new ProductViewModel
            {
                Products = productRepository.GetProducts()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var viewModel = new ProductViewModel
            {
                ProductModel = productRepository.GetProductById(id),
                Suppliers = supplierRepository.GetSuppliers(),
                Categories = categoryRepository.GetCategories()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel viewModel)
        {
	        throw new NotImplementedException();

        }
    }
}
