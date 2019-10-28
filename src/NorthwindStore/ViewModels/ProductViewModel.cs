using NorthwindStore.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindStore.ViewModels
{
    public class ProductViewModel
    {
        public Products ProductModel { get; set; }

        public IEnumerable<Products> Products { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
