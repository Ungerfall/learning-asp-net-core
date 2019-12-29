using NorthwindStore.Test.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace NorthwindStore.Test.API
{
    public class ApiTest
    {
        private static HttpClient client = new HttpClient();

        static ApiTest()
        {
            client.BaseAddress = new Uri("https://localhost:44339/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task ApiProductsGet_ReturnsJson_WithAllProducts()
        {
            IEnumerable<Products> products = null;
            var response = await client.GetAsync("product/");

            Assert.True(response.IsSuccessStatusCode);

            products = await response.Content.ReadAsAsync<Products[]>();

            Assert.Equal(82, products.Count());
        }

        [Fact]
        public async Task ApiCategoriesGet_ReturnsJson_WithAllCategories()
        {
	        IEnumerable<Categories> categories = null;
	        var response = await client.GetAsync("category/");

            Assert.True(response.IsSuccessStatusCode);

            categories = await response.Content.ReadAsAsync<Categories[]>();

            Assert.Equal(8, categories.Count());
        }
    }
}
