using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lennujaam_MVC.Data;
using Lennujaam_MVC.Models;

namespace Lennujaam_MVC.Controllers
{
    public class LennudController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LennudController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lennud
        public async Task<IActionResult> Index()
        {
            return _context.Lennud != null ?
                        View(await _context.Lennud.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
        }

        // GET: Lennud/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lennud == null)
            {
                return NotFound();
            }

            var lend = await _context.Lennud
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lend == null)
            {
                return NotFound();
            }

            return View(lend);
        }

        // GET: Lennud/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lennud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,KohtadeArv,ReisijateArv,Otspunkt,Sihtpunkt,ValjumisAeg,Lopetatud,Kestvus")] Lend lend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lend);
        }

        // GET: Lennud/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lennud == null)
            {
                return NotFound();
            }

            var lend = await _context.Lennud.FindAsync(id);
            if (lend == null)
            {
                return NotFound();
            }
            return View(lend);
        }

        // POST: Lennud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,KohtadeArv,ReisijateArv,Otspunkt,Sihtpunkt,ValjumisAeg,Lopetatud,Kestvus")] Lend lend)
        {
            if (id != lend.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LendExists(lend.ID))
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
            return View(lend);
        }

        // GET: Lennud/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lennud == null)
            {
                return NotFound();
            }

            var lend = await _context.Lennud
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lend == null)
            {
                return NotFound();
            }

            return View(lend);
        }

        // POST: Lennud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lennud == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
            }
            var lend = await _context.Lennud.FindAsync(id);
            if (lend != null)
            {
                _context.Lennud.Remove(lend);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LendExists(int id)
        {
            return (_context.Lennud?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
