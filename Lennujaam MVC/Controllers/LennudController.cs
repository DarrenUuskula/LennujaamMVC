using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> Edit(int id, [Bind("ID,KohtadeArv,ReisijateArv,Otspunkt,Sihtpunkt,ValjumisAeg,Lopetatud")] Lend lend)
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

		// GET: Lennud/Edit/5
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

		// POST: Lennud/Edit/5
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

		public IActionResult LisaLend()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LisaLend([Bind("ID,KohtadeArv,Otspunkt,Sihtpunkt,ValjumisAeg")] Lend lend)
		{
			if (ModelState.IsValid)
			{
				_context.Add(lend);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(lend);
		}

		public async Task<IActionResult> Index()
		{
			return _context.Lennud != null ?
			View(await _context.Lennud.ToListAsync()) :
			Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
		}

		public async Task<IActionResult> LisaReisijaid()
		{
			return _context.Lennud != null ?
			View(await _context.Lennud.Where(lend => lend.ReisijateArv < lend.KohtadeArv && !lend.Lopetatud).ToListAsync()) :
			Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
		}

		public async Task<IActionResult> LisaReisija(int id)
		{
			if (_context.Lennud == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
			}
			var lend = await _context.Lennud.FindAsync(id);
			if (lend != null)
			{
				lend.ReisijateArv++;
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(LisaReisijaid));
		}

		public async Task<IActionResult> Lopetamata()
		{
			return _context.Lennud != null ?
			View(await _context.Lennud.Where(lend => !lend.Lopetatud).ToListAsync()) :
			Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
		}

		public async Task<IActionResult> Lopeta(int id)
		{
			if (_context.Lennud == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
			}
			var lend = await _context.Lennud.FindAsync(id);
			if (lend != null)
			{
				lend.Lopetatud = true;
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Lopetamata));
		}

		public async Task<IActionResult> Kestvuseta()
		{
			return _context.Lennud != null ?
			View(await _context.Lennud.Where(lend => lend.Kestvus == TimeSpan.Zero && lend.Lopetatud).ToListAsync()) :
			Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
		}

		[HttpPost]
		public async Task<IActionResult> LisaKestvus(int id, int days, int hours, int minutes)
		{
			if (_context.Lennud == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
			}
			var lend = await _context.Lennud.FindAsync(id);
			if (lend != null)
			{
				lend.Kestvus = new TimeSpan(days, hours, minutes);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Kestvuseta));
		}

		public async Task<IActionResult> Lopetatud()
		{
			return _context.Lennud != null ?
			View(await _context.Lennud.Where(lend => lend.Lopetatud && lend.Kestvus != TimeSpan.Zero).ToListAsync()) :
			Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
		}

		public async Task<IActionResult> Statistika()
		{
			return _context.Lennud != null ?
			View(new StatistikaViewModel()
			{
				Teenindatud_Summa = await _context.Lennud.Where(lend => lend.Lopetatud).SumAsync(lend => lend.ReisijateArv)
			}) :
			Problem("Entity set 'ApplicationDbContext.Lennud'  is null.");
		}

		private bool LendExists(int id)
		{
			return (_context.Lennud?.Any(e => e.ID == id)).GetValueOrDefault();
		}
	}
}
