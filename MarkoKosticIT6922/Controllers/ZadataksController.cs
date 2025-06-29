using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarkoKosticIT6922.Data;
using MarkoKosticIT6922.Models;
using Microsoft.AspNetCore.Authorization;

namespace MarkoKosticIT6922.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ZadataksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZadataksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zadataks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zadaci.Include(z => z.Igra).Include(z => z.Uloga);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zadataks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadatak = await _context.Zadaci
                .Include(z => z.Igra)
                .Include(z => z.Uloga)
                .FirstOrDefaultAsync(m => m.ZadatakId == id);
            if (zadatak == null)
            {
                return NotFound();
            }

            return View(zadatak);
        }

        // GET: Zadataks/Create
        public IActionResult Create()
        {
            ViewData["IgraId"] = new SelectList(_context.Igre, "IgraId", "IgraId");
            ViewData["UlogaId"] = new SelectList(_context.Uloge, "UlogaId", "UlogaId");
            return View();
        }

        // POST: Zadataks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZadatakId,Naziv,Opis,IgraId,UlogaId,Rok,Reseno")] Zadatak zadatak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zadatak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IgraId"] = new SelectList(_context.Igre, "IgraId", "IgraId", zadatak.IgraId);
            ViewData["UlogaId"] = new SelectList(_context.Uloge, "UlogaId", "UlogaId", zadatak.UlogaId);
            return View(zadatak);
        }

        // GET: Zadataks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadatak = await _context.Zadaci.FindAsync(id);
            if (zadatak == null)
            {
                return NotFound();
            }
            ViewData["IgraId"] = new SelectList(_context.Igre, "IgraId", "IgraId", zadatak.IgraId);
            ViewData["UlogaId"] = new SelectList(_context.Uloge, "UlogaId", "UlogaId", zadatak.UlogaId);
            return View(zadatak);
        }

        // POST: Zadataks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZadatakId,Naziv,Opis,IgraId,UlogaId,Rok,Reseno")] Zadatak zadatak)
        {
            if (id != zadatak.ZadatakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zadatak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZadatakExists(zadatak.ZadatakId))
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
            ViewData["IgraId"] = new SelectList(_context.Igre, "IgraId", "IgraId", zadatak.IgraId);
            ViewData["UlogaId"] = new SelectList(_context.Uloge, "UlogaId", "UlogaId", zadatak.UlogaId);
            return View(zadatak);
        }

        // GET: Zadataks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadatak = await _context.Zadaci
                .Include(z => z.Igra)
                .Include(z => z.Uloga)
                .FirstOrDefaultAsync(m => m.ZadatakId == id);
            if (zadatak == null)
            {
                return NotFound();
            }

            return View(zadatak);
        }

        // POST: Zadataks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zadatak = await _context.Zadaci.FindAsync(id);
            if (zadatak != null)
            {
                _context.Zadaci.Remove(zadatak);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZadatakExists(int id)
        {
            return _context.Zadaci.Any(e => e.ZadatakId == id);
        }
    }
}
