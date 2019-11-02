using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using NorthwindStore.Data;
using NorthwindStore.Data.Models;
using NorthwindStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IMemoryCache cache;

        public ProductController(
            ISupplierRepository supplierRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IMemoryCache cache)
        {
            this.supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
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

            var suppliers = GetSuppliers();
            var categories = GetCategories();
            var viewModel = new ProductViewModel
            {
                ProductModel = productRepository.GetProductById(id),
                Suppliers = suppliers,
                Categories = categories
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

                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = GetCategories();
            viewModel.Suppliers = GetSuppliers();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var suppliers = GetSuppliers();
            var categories = GetCategories();
            var viewModel = new ProductViewModel
            {
                ProductModel = new Products(),
                Suppliers = suppliers,
                Categories = categories
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

                return RedirectToAction(nameof(Index));
            }

            viewModel.Suppliers = GetSuppliers();
            viewModel.Categories = GetCategories();
            return View(viewModel);
        }

        private IEnumerable<SelectListItem> GetSuppliers()
        {
            return cache.GetOrCreate(
                "suppliers",
                x => supplierRepository.GetSuppliers()
                    .Select(s => new SelectListItem
                    {
                        Text = s.CompanyName,
                        Value = s.SupplierId.ToString(),
                    })
                    .ToList());
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            return cache.GetOrCreate(
                "categories",
                x => categoryRepository.GetCategories()
                    .Select(c => new SelectListItem
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                    .ToList());
        }
    }
}
