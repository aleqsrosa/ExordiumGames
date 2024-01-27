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
    public class ItemsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsVM>>> GetItems()
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            return await _context.Items.ToListAsync();
        }

        // GET: api/ItemsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemsVM>> GetItemsVM(int id)
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            var itemsVM = await _context.Items.FindAsync(id);

            if (itemsVM == null)
            {
                return NotFound();
            }

            return itemsVM;
        }

        // PUT: api/ItemsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemsVM(int id, ItemsVM itemsVM)
        {
            if (id != itemsVM.ID)
            {
                return BadRequest();
            }

            _context.Entry(itemsVM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsVMExists(id))
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

        // POST: api/ItemsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemsVM>> PostItemsVM(ItemsVM itemsVM)
        {
          if (_context.Items == null)
          {
              return Problem("Entity set 'AppDbContext.Items'  is null.");
          }
            _context.Items.Add(itemsVM);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemsVM", new { id = itemsVM.ID }, itemsVM);
        }

        // DELETE: api/ItemsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemsVM(int id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var itemsVM = await _context.Items.FindAsync(id);
            if (itemsVM == null)
            {
                return NotFound();
            }

            _context.Items.Remove(itemsVM);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemsVMExists(int id)
        {
            return (_context.Items?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
