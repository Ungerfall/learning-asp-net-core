using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindStore.Data;
using NorthwindStore.Data.Models;
using NorthwindStore.ViewModels;
using System;
using System.Linq;

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
                Suppliers = supplierRepository.GetSuppliers()
                    .Select(x => new SelectListItem
                    {
                        Text = x.CompanyName,
                        Value = x.SupplierId.ToString(),
                    }),
                Categories = categoryRepository.GetCategories()
                    .Select(x => new SelectListItem
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryId.ToString()
                    })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                productRepository.UpdateProduct(viewModel.ProductModel);
                productRepository.Save();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ProductViewModel
            {
                ProductModel = new Products(),
                Suppliers = supplierRepository.GetSuppliers()
                    .Select(x => new SelectListItem
                    {
                        Text = x.CompanyName,
                        Value = x.SupplierId.ToString(),
                    }),
                Categories = categoryRepository.GetCategories()
                    .Select(x => new SelectListItem
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryId.ToString()
                    })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                productRepository.InsertProduct(viewModel.ProductModel);
                productRepository.Save();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
