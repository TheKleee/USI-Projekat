using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarkoKosticIT6922.Data;
using MarkoKosticIT6922.Models;

namespace MarkoKosticIT6922.Controllers
{
    public class ResenjesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResenjesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Resenjes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Resenja.Include(r => r.Korisnik).Include(r => r.Zadatak);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Resenjes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenje = await _context.Resenja
                .Include(r => r.Korisnik)
                .Include(r => r.Zadatak)
                .FirstOrDefaultAsync(m => m.ResenjeId == id);
            if (resenje == null)
            {
                return NotFound();
            }

            return View(resenje);
        }

        // GET: Resenjes/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ZadatakId"] = new SelectList(_context.Zadaci, "ZadatakId", "ZadatakId");
            return View();
        }

        // POST: Resenjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResenjeId,Opis,ZadatakId,KorisnikId,Odobreno")] Resenje resenje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resenje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id", resenje.KorisnikId);
            ViewData["ZadatakId"] = new SelectList(_context.Zadaci, "ZadatakId", "ZadatakId", resenje.ZadatakId);
            return View(resenje);
        }

        // GET: Resenjes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenje = await _context.Resenja.FindAsync(id);
            if (resenje == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id", resenje.KorisnikId);
            ViewData["ZadatakId"] = new SelectList(_context.Zadaci, "ZadatakId", "ZadatakId", resenje.ZadatakId);
            return View(resenje);
        }

        // POST: Resenjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResenjeId,Opis,ZadatakId,KorisnikId,Odobreno")] Resenje resenje)
        {
            if (id != resenje.ResenjeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resenje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResenjeExists(resenje.ResenjeId))
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
            ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id", resenje.KorisnikId);
            ViewData["ZadatakId"] = new SelectList(_context.Zadaci, "ZadatakId", "ZadatakId", resenje.ZadatakId);
            return View(resenje);
        }

        // GET: Resenjes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resenje = await _context.Resenja
                .Include(r => r.Korisnik)
                .Include(r => r.Zadatak)
                .FirstOrDefaultAsync(m => m.ResenjeId == id);
            if (resenje == null)
            {
                return NotFound();
            }

            return View(resenje);
        }

        // POST: Resenjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resenje = await _context.Resenja.FindAsync(id);
            if (resenje != null)
            {
                _context.Resenja.Remove(resenje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResenjeExists(int id)
        {
            return _context.Resenja.Any(e => e.ResenjeId == id);
        }
    }
}
