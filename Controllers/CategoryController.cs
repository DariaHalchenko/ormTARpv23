using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Category
        [HttpGet]
        public List<Category> GetCategories()
        {
            return _context.Categorys.ToList();
        }

        // GET: /Category/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _context.Categorys.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        // POST: /Category
        [HttpPost]
        public ActionResult<List<Category>> PostCategory([FromBody] Category category)
        {
            _context.Categorys.Add(category);
            _context.SaveChanges();
            return _context.Categorys.ToList();
        }

        // PUT: /Category/{id}
        [HttpPut("{id}")]
        public ActionResult<List<Category>> PutCategory(int id, [FromBody] Category updatedCategory)
        {
            var category = _context.Categorys.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;

            _context.Categorys.Update(category);
            _context.SaveChanges();

            return _context.Categorys.ToList();
        }

        // DELETE: /Category/{id}
        [HttpDelete("{id}")]
        public ActionResult<List<Category>> DeleteCategory(int id)
        {
            var category = _context.Categorys.Find(id);
            if (category == null)
            {
                return _context.Categorys.ToList();
            }

            _context.Categorys.Remove(category);
            _context.SaveChanges();

            return _context.Categorys.ToList();
        }
    }
}
