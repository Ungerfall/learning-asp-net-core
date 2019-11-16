using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NorthwindStore.Controllers;
using NorthwindStore.Data;
using NorthwindStore.Data.Models;
using NorthwindStore.ViewModels;
using System.Linq;
using NorthwindStore.Test.Comparers;
using Xunit;

namespace NorthwindStore.Test
{
    public class ControllerTest
    {
        [Fact]
        public void CategoryIndex_ReturnsAViewResult_WithAllCategories()
        {
            int count = 10;
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo
                .Setup(x => x.GetCategories())
                .Returns(GetCategories(count));
            var controller = new CategoryController(mockRepo.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IQueryable<Categories>>(viewResult.ViewData.Model);
            Assert.Equal(count, model.Count());
        }

        [Fact]
        public void ProductIndex_ReturnsAViewResult_WithProducts()
        {
            int count = 10;
            var mockCategoryRepo = Mock.Of<ICategoryRepository>();
            var mockSupplierRepo = Mock.Of<ISupplierRepository>();
            var mockCache = Mock.Of<IMemoryCache>();
            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(x => x.GetProducts())
                .Returns(GetProducts(count));
            var controller = new ProductController(
                mockSupplierRepo,
                mockCategoryRepo,
                mockRepo.Object,
                mockCache);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);
            Assert.Equal(count, model.Products.Count());
        }

        [Fact]
        public void ProductEdit_NullId_ReturnsNotFound()
        {
            var mockCategoryRepo = Mock.Of<ICategoryRepository>();
            var mockSupplierRepo = Mock.Of<ISupplierRepository>();
            var mockProductRepo = Mock.Of<IProductRepository>();
            var mockCache = Mock.Of<IMemoryCache>();
            var controller = new ProductController(
                mockSupplierRepo,
                mockCategoryRepo,
                mockProductRepo,
                mockCache);

            var result = controller.Edit(id: null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ProductEdit_ReturnsAViewResult_WithCategoriesSuppliersAndOneProduct()
        {
            int count = 10;
            int productId = 3;
            var product = new Products
            {
                SupplierId = 3,
                CategoryId = 4,
                Discontinued = false,
                ProductId = productId,
                ProductName = "test product",
                QuantityPerUnit = "100",
                UnitPrice = 10m,
                UnitsInStock = (short)5000,
                UnitsOnOrder = (short)400
            };
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo
                .Setup(x => x.GetCategories())
                .Returns(GetCategories(count));
            var mockSupplierRepo = new Mock<ISupplierRepository>();
            mockSupplierRepo
                .Setup(x => x.GetSuppliers())
                .Returns(GetSuppliers(count));
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo
                .Setup(x => x.GetProductById(productId))
                .Returns(product);
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            memoryCache.GetOrCreate("suppliers", c => GetSuppliers(count)
                .Select(x => new SelectListItem
                {
                    Text = x.CompanyName,
                    Value = x.SupplierId.ToString()
                })
                .ToList());
            memoryCache.GetOrCreate("categories", c => GetCategories(count)
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                })
                .ToList());
            var controller = new ProductController(
                mockSupplierRepo.Object,
                mockCategoryRepo.Object,
                mockProductRepo.Object,
                memoryCache);

            var result = controller.Edit(productId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);
            Assert.Equal(count, model.Categories.Count());
            Assert.Equal(count, model.Suppliers.Count());
            Assert.Equal(product, model.ProductModel);
        }

        [Fact]
        public void ProductEditPost_RedirectsToIndex_WhenModelIsValid()
        {
            var mockCategoryRepo = Mock.Of<ICategoryRepository>();
            var mockSupplierRepo = Mock.Of<ISupplierRepository>();
            var mockProductRepo = Mock.Of<IProductRepository>();
            var mockCache = Mock.Of<IMemoryCache>();
            var controller = new ProductController(
                mockSupplierRepo,
                mockCategoryRepo,
                mockProductRepo,
                mockCache);
            var viewModel = new ProductViewModel();

            var result = controller.Edit(viewModel);

            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToAction.ControllerName);
            Assert.Equal("Index", redirectToAction.ActionName);
        }

        [Fact]
        public void ProductCreate_ReturnsAViewResult_WithCategoriesSuppliersAndEmptyProduct()
        {
            int count = 5;
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo
                .Setup(x => x.GetCategories())
                .Returns(GetCategories(count));
            var mockSupplierRepo = new Mock<ISupplierRepository>();
            mockSupplierRepo
                .Setup(x => x.GetSuppliers())
                .Returns(GetSuppliers(count));
            var mockProductRepo = Mock.Of<IProductRepository>();
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            memoryCache.GetOrCreate("suppliers", c => GetSuppliers(count)
                .Select(x => new SelectListItem
                {
                    Text = x.CompanyName,
                    Value = x.SupplierId.ToString()
                })
                .ToList());
            memoryCache.GetOrCreate("categories", c => GetCategories(count)
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                })
                .ToList());
            var controller = new ProductController(
                mockSupplierRepo.Object,
                mockCategoryRepo.Object,
                mockProductRepo,
                memoryCache);

            var result = controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);
            Assert.Equal(count, model.Categories.Count());
            Assert.Equal(count, model.Suppliers.Count());
            Assert.Equal(new Products(), model.ProductModel, new ProductsEqualityComparer());
        }

        [Fact]
        public void ProductCreatePost_RedirectsToIndex_WhenModelIsValid()
        {
            var mockCategoryRepo = Mock.Of<ICategoryRepository>();
            var mockSupplierRepo = Mock.Of<ISupplierRepository>();
            var mockProductRepo = Mock.Of<IProductRepository>();
            var mockCache = Mock.Of<IMemoryCache>();
            var controller = new ProductController(
                mockSupplierRepo,
                mockCategoryRepo,
                mockProductRepo,
                mockCache);
            var viewModel = new ProductViewModel();

            var result = controller.Edit(viewModel);

            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToAction.ControllerName);
            Assert.Equal("Index", redirectToAction.ActionName);
        }

        public static IQueryable<Suppliers> GetSuppliers(int count)
        {
            return new EnumerableQuery<Suppliers>(
                Enumerable
                    .Range(1, count)
                    .Select(x => new Suppliers { SupplierId = x }));
        }

        public static IQueryable<Products> GetProducts(int count)
        {
            return new EnumerableQuery<Products>(
                Enumerable
                    .Range(1, count)
                    .Select(x => new Products { ProductId = x }));
        }

        public static IQueryable<Categories> GetCategories(int count)
        {
            return new EnumerableQuery<Categories>(
                Enumerable
                    .Range(1, count)
                    .Select(x => new Categories { CategoryId = x }));
        }
    }
}
