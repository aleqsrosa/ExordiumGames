using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExordiumGames.Data;
using ExordiumGames.Models;

namespace ExordiumGames.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
              return _context.Items != null ? 
                          View(await _context.Items.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Items'  is null.");
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var itemsVM = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (itemsVM == null)
            {
                return NotFound();
            }

            return View(itemsVM);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazivproizvoda,Opis,Datumakcije,Nazivretailera,URLdoslike,Cijena,Kategorija")] ItemsVM itemsVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemsVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemsVM);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var itemsVM = await _context.Items.FindAsync(id);
            if (itemsVM == null)
            {
                return NotFound();
            }
            return View(itemsVM);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazivproizvoda,Opis,Datumakcije,Nazivretailera,URLdoslike,Cijena,Kategorija")] ItemsVM itemsVM)
        {
            if (id != itemsVM.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemsVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsVMExists(itemsVM.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemsVM);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var itemsVM = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (itemsVM == null)
            {
                return NotFound();
            }

            return View(itemsVM);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'AppDbContext.Items'  is null.");
            }
            var itemsVM = await _context.Items.FindAsync(id);
            if (itemsVM != null)
            {
                _context.Items.Remove(itemsVM);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsVMExists(int id)
        {
          return (_context.Items?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
