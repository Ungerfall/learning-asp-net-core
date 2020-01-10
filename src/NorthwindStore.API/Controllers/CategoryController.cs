using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindStore.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly NorthwindContext context;

        public CategoryController(NorthwindContext context)
        {
            this.context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}.{format?}")]
        public async Task<ActionResult<Categories>> GetCategory(int id, string format = null)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            if (format != null)
            {
                byte[] image = category.AlignedPicture;
                return File(image, "image/bmp");
            }

            return category;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Categories category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            context.Entry(category).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategory(Categories category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categories>> DeleteCategory(int id)
        {
            var categories = await context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            context.Categories.Remove(categories);
            await context.SaveChangesAsync();

            return categories;
        }

        private bool CategoriesExists(int id)
        {
            return context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
