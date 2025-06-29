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
    public class GreskasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GreskasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Greskas
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Greske.Include(g => g.Korisnik).Include(g => g.Resenje);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Greskas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var greska = await _context.Greske
                .Include(g => g.Korisnik)
                .Include(g => g.Resenje)
                .FirstOrDefaultAsync(m => m.GreskaId == id);
            if (greska == null)
            {
                return NotFound();
            }

            return View(greska);
        }

        // GET: Greskas/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ResenjeId"] = new SelectList(_context.Resenja, "ResenjeId", "ResenjeId");
            return View();
        }

        // POST: Greskas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GreskaId,Opis,KorisnikId,ResenjeId")] Greska greska)
        {
            if (ModelState.IsValid)
            {
                _context.Add(greska);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id", greska.KorisnikId);
            ViewData["ResenjeId"] = new SelectList(_context.Resenja, "ResenjeId", "ResenjeId", greska.ResenjeId);
            return View(greska);
        }

        // GET: Greskas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var greska = await _context.Greske.FindAsync(id);
            if (greska == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id", greska.KorisnikId);
            ViewData["ResenjeId"] = new SelectList(_context.Resenja, "ResenjeId", "ResenjeId", greska.ResenjeId);
            return View(greska);
        }

        // POST: Greskas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GreskaId,Opis,KorisnikId,ResenjeId")] Greska greska)
        {
            if (id != greska.GreskaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(greska);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GreskaExists(greska.GreskaId))
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
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id", greska.KorisnikId);
            ViewData["ResenjeId"] = new SelectList(_context.Resenja, "ResenjeId", "ResenjeId", greska.ResenjeId);
            return View(greska);
        }

        // GET: Greskas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var greska = await _context.Greske
                .Include(g => g.Korisnik)
                .Include(g => g.Resenje)
                .FirstOrDefaultAsync(m => m.GreskaId == id);
            if (greska == null)
            {
                return NotFound();
            }

            return View(greska);
        }

        // POST: Greskas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var greska = await _context.Greske.FindAsync(id);
            if (greska != null)
            {
                _context.Greske.Remove(greska);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GreskaExists(int id)
        {
            return _context.Greske.Any(e => e.GreskaId == id);
        }
    }
}
