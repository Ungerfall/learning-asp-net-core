using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NorthwindStore.Data.Models;

namespace NorthwindStore.ViewModels
{
    public class CategoryViewModel
    {
        public Categories CategoryModel { get; set; }

        [Display(Name = "Picture")]
        public IFormFile CategoryImageFile { get; set; }
    }
}
