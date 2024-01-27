using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExordiumGames.Data;
using ExordiumGames.Models;

namespace ExordiumGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoryApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryVM>>> GetCategory()
        {
          if (_context.Category == null)
          {
              return NotFound();
          }
            return await _context.Category.ToListAsync();
        }

        // GET: api/CategoryApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVM>> GetCategoryVM(int id)
        {
          if (_context.Category == null)
          {
              return NotFound();
          }
            var categoryVM = await _context.Category.FindAsync(id);

            if (categoryVM == null)
            {
                return NotFound();
            }

            return categoryVM;
        }

        // PUT: api/CategoryApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryVM(int id, CategoryVM categoryVM)
        {
            if (id != categoryVM.ID)
            {
                return BadRequest();
            }

            _context.Entry(categoryVM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryVMExists(id))
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

        // POST: api/CategoryApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryVM>> PostCategoryVM(CategoryVM categoryVM)
        {
          if (_context.Category == null)
          {
              return Problem("Entity set 'AppDbContext.Category'  is null.");
          }
            _context.Category.Add(categoryVM);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryVM", new { id = categoryVM.ID }, categoryVM);
        }

        // DELETE: api/CategoryApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryVM(int id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var categoryVM = await _context.Category.FindAsync(id);
            if (categoryVM == null)
            {
                return NotFound();
            }

            _context.Category.Remove(categoryVM);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryVMExists(int id)
        {
            return (_context.Category?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
