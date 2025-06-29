using MarkoKosticIT6922.Data;
using MarkoKosticIT6922.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkoKosticIT6922.Controllers
{
    [Authorize]
    public class IgraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IgraController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var igre = await _context.Igre.ToListAsync();

            return View(igre);
        }
        public async Task<IActionResult> Zadaci(int id)
        {
            var zadaci = await _context.Igre
                    .Include(i => i.Zadaci)
                    .FirstOrDefaultAsync(i => i.IgraId == id);

            if (zadaci == null) return NotFound();

            return View(zadaci);
        }

        public async Task<IActionResult> Resenja(int id)
        {
            var resenja = await _context.Zadaci
                    .Include(z => z.Resenja)
                    .FirstOrDefaultAsync(z => z.ZadatakId == id);

            if (resenja == null) return NotFound();

            return View(resenja);
        }

        public async Task<IActionResult> Test(int id)
        {
            var resenje = await _context.Resenja
                    .FirstOrDefaultAsync(z => z.ResenjeId == id);

            if (resenje == null) return NotFound();

            return View(resenje);
        }

        public async Task<IActionResult> Odobri(int id)
        {
            var resenZadatak = await _context.Zadaci
                .FirstOrDefaultAsync(z => z.ZadatakId == id);

            if (resenZadatak == null) return NotFound();

            resenZadatak.Reseno = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Zadaci", new { id = resenZadatak.IgraId });
        }

        [HttpGet]
        public async Task<IActionResult> Greska(int id)
        {
            var resenje = await _context.Resenja
                .FirstOrDefaultAsync(z => z.ResenjeId == id);

            if (resenje == null) return NotFound();

            var greska = new Greska
            {
                Opis = "",
                ResenjeId = id,
                Resenje = resenje
            };

            return View(greska);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Greska(Greska greska)
        {
            if (!ModelState.IsValid)
            {
                greska.Resenje = await _context.Resenja.FindAsync(greska.ResenjeId);
                return View("Greska", greska);
            }

            var korisnikId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (korisnikId == null)
            {
                return Unauthorized();
            }

            greska.KorisnikId = korisnikId;


            _context.Greske.Add(greska);
            await _context.SaveChangesAsync();

            return RedirectToAction("Resenja", new { id = greska.Resenje?.ZadatakId });
        }
    }
}
