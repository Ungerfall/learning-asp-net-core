using NorthwindStore.Data.Models;
using System.Collections.Generic;

namespace NorthwindStore.ViewModels
{
    public class ProductViewModel
    {
        public Products ProductModel { get; set; }

        public IEnumerable<Products> Products { get; set; }

        public IEnumerable<Suppliers> Suppliers { get; set; }

        public IEnumerable<Categories> Categories { get; set; }
    }
}
