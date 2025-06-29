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
    public class IgrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IgrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Igras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Igre.ToListAsync());
        }

        // GET: Igras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igra = await _context.Igre
                .FirstOrDefaultAsync(m => m.IgraId == id);
            if (igra == null)
            {
                return NotFound();
            }

            return View(igra);
        }

        // GET: Igras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Igras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IgraId,Naziv,Opis")] Igra igra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(igra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(igra);
        }

        // GET: Igras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igra = await _context.Igre.FindAsync(id);
            if (igra == null)
            {
                return NotFound();
            }
            return View(igra);
        }

        // POST: Igras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IgraId,Naziv,Opis")] Igra igra)
        {
            if (id != igra.IgraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgraExists(igra.IgraId))
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
            return View(igra);
        }

        // GET: Igras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igra = await _context.Igre
                .FirstOrDefaultAsync(m => m.IgraId == id);
            if (igra == null)
            {
                return NotFound();
            }

            return View(igra);
        }

        // POST: Igras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var igra = await _context.Igre.FindAsync(id);
            if (igra != null)
            {
                _context.Igre.Remove(igra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgraExists(int id)
        {
            return _context.Igre.Any(e => e.IgraId == id);
        }
    }
}
