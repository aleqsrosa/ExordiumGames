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
    public class RetailerController : Controller
    {
        private readonly AppDbContext _context;

        public RetailerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Retailer
        public async Task<IActionResult> Index()
        {
              return _context.Retailer != null ? 
                          View(await _context.Retailer.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Retailer'  is null.");
        }

        // GET: Retailer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Retailer == null)
            {
                return NotFound();
            }

            var retailerVM = await _context.Retailer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (retailerVM == null)
            {
                return NotFound();
            }

            return View(retailerVM);
        }

        // GET: Retailer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Retailer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Priority,LogoImageUrl")] RetailerVM retailerVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(retailerVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(retailerVM);
        }

        // GET: Retailer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Retailer == null)
            {
                return NotFound();
            }

            var retailerVM = await _context.Retailer.FindAsync(id);
            if (retailerVM == null)
            {
                return NotFound();
            }
            return View(retailerVM);
        }

        // POST: Retailer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Priority,LogoImageUrl")] RetailerVM retailerVM)
        {
            if (id != retailerVM.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retailerVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetailerVMExists(retailerVM.ID))
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
            return View(retailerVM);
        }

        // GET: Retailer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Retailer == null)
            {
                return NotFound();
            }

            var retailerVM = await _context.Retailer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (retailerVM == null)
            {
                return NotFound();
            }

            return View(retailerVM);
        }

        // POST: Retailer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Retailer == null)
            {
                return Problem("Entity set 'AppDbContext.Retailer'  is null.");
            }
            var retailerVM = await _context.Retailer.FindAsync(id);
            if (retailerVM != null)
            {
                _context.Retailer.Remove(retailerVM);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetailerVMExists(int id)
        {
          return (_context.Retailer?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
