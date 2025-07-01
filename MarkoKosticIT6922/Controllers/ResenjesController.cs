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
using Microsoft.AspNetCore.Identity;

namespace MarkoKosticIT6922.Controllers
{
    public class ResenjesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public ResenjesController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Resenjes
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Resenja.Include(r => r.Korisnik).Include(r => r.Zadatak);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Resenjes/Details/5
        [Authorize(Roles = "Admin")]
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
        public IActionResult Create(int? zadatakId)
        {
            if (User.IsInRole("Admin"))
            {
                ViewData["KorisnikId"] = new SelectList(_context.Users, "Id", "Id");
                ViewData["ZadatakId"] = new SelectList(_context.Zadaci, "ZadatakId", "ZadatakId");
            } else {
                ViewBag.ZadatakId = zadatakId;
                var korisnikId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                ViewBag.KorisnikId = korisnikId;
            }

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                ViewBag.ReturnUrl = referer;
            
            return View();
        }

        // POST: Resenjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResenjeId,Opis,ZadatakId,KorisnikId,Odobreno")] Resenje resenje, string? returnUrl)
        {
            var korisnik = await _userManager.GetUserAsync(User);

            if (!User.IsInRole("Admin") && korisnik != null)
                resenje.KorisnikId = korisnik.Id;

            var zadatak = await _context.Zadaci.FirstOrDefaultAsync(z => z.ZadatakId == resenje.ZadatakId);

            if (zadatak == null)
                ModelState.AddModelError(string.Empty, "Zadatak nije pronadjen.");
            else if (korisnik != null && korisnik.UlogaId != zadatak.UlogaId && !User.IsInRole("Admin"))
                ModelState.AddModelError(string.Empty, "Nemate dozvolu da dodate resenje za ovaj zadatak.");

            if (ModelState.IsValid)
            {
                _context.Add(resenje);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

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
            
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                ViewBag.ReturnUrl = referer;

            return View(resenje);
        }

        // POST: Resenjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResenjeId,Opis,ZadatakId,KorisnikId,Odobreno")] Resenje resenje, string? returnUrl)
        {
            if (id != resenje.ResenjeId)
            {
                return NotFound();
            }

            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null)
                ModelState.AddModelError(string.Empty, "Korisnik nije pronadjen.");

            var zadatak = await _context.Zadaci.FirstOrDefaultAsync(z => z.ZadatakId == resenje.ZadatakId);

            if (zadatak == null)
                ModelState.AddModelError(string.Empty, "Zadatak nije pronađen.");
            else if (korisnik != null && korisnik.UlogaId != zadatak.UlogaId && !User.IsInRole("Admin"))
                ModelState.AddModelError(string.Empty, "Nemate dozvolu da izmenite resenje za ovaj zadatak.");

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

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

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

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                ViewBag.ReturnUrl = referer;

            return View(resenje);
        }

        // POST: Resenjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? returnUrl)
        {
            var resenje = await _context.Resenja.FindAsync(id);
            if (resenje != null)
            {
                _context.Resenja.Remove(resenje);
            }

            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null)
                ModelState.AddModelError(string.Empty, "Korisnik nije pronadjen.");

            var zadatak = await _context.Zadaci.FirstOrDefaultAsync(z => z.ZadatakId == resenje.ZadatakId);

            if (zadatak == null)
                ModelState.AddModelError(string.Empty, "Zadatak nije pronađen.");
            else if (korisnik != null && korisnik.UlogaId != zadatak.UlogaId && !User.IsInRole("Admin"))
                ModelState.AddModelError(string.Empty, "Nemate dozvolu da brisete resenje za ovaj zadatak.");

            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            

            return RedirectToAction(nameof(Index));
        }

        private bool ResenjeExists(int id)
        {
            return _context.Resenja.Any(e => e.ResenjeId == id);
        }
    }
}
