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
    public class UlogasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UlogasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ulogas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uloge.ToListAsync());
        }

        // GET: Ulogas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uloga = await _context.Uloge
                .FirstOrDefaultAsync(m => m.UlogaId == id);
            if (uloga == null)
            {
                return NotFound();
            }

            return View(uloga);
        }

        // GET: Ulogas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ulogas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UlogaId,Naziv")] Uloga uloga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uloga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uloga);
        }

        // GET: Ulogas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uloga = await _context.Uloge.FindAsync(id);
            if (uloga == null)
            {
                return NotFound();
            }
            return View(uloga);
        }

        // POST: Ulogas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UlogaId,Naziv")] Uloga uloga)
        {
            if (id != uloga.UlogaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uloga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UlogaExists(uloga.UlogaId))
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
            return View(uloga);
        }

        // GET: Ulogas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uloga = await _context.Uloge
                .FirstOrDefaultAsync(m => m.UlogaId == id);
            if (uloga == null)
            {
                return NotFound();
            }

            return View(uloga);
        }

        // POST: Ulogas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uloga = await _context.Uloge.FindAsync(id);
            if (uloga != null)
            {
                _context.Uloge.Remove(uloga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UlogaExists(int id)
        {
            return _context.Uloge.Any(e => e.UlogaId == id);
        }
    }
}
