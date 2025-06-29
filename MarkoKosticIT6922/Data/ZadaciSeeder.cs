
using MarkoKosticIT6922.Models;

namespace MarkoKosticIT6922.Data
{
    internal class ZadaciSeeder
    {
        internal static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Zadaci.Any())
            {
                var igra1 = context.Igre.FirstOrDefault(i => i.Naziv == "RPG Tower");
                var igra2 = context.Igre.FirstOrDefault(i => i.Naziv == "Crazy War");
                var programer = context.Uloge.FirstOrDefault(u => u.Naziv == "Programer");
                var dizajner = context.Uloge.FirstOrDefault(u => u.Naziv == "Dizajner");
                if (igra1 != null && igra2 != null && programer != null && dizajner != null)
                    context.Zadaci.AddRange(
                        new Zadatak { Naziv = "Nadograditi kulu", Opis = "Nadograditi kulu da moze da se pomera s gusenicama.", IgraId = igra1.IgraId, UlogaId = programer.UlogaId },
                        new Zadatak { Naziv = "Prepreke prirode", Opis = "Dostaviti prirodne prepreke ali ostaviti prostranstvo otvorenim.", IgraId = igra1.IgraId, UlogaId = dizajner.UlogaId },
                        new Zadatak { Naziv = "Prepreke prirode", Opis = "Napraviti jasne indikatore ciljeva u toku partije.", UlogaId = dizajner.UlogaId, IgraId = igra1.IgraId },
                        new Zadatak { Naziv = "Prepreke prirode", Opis = "Napraviti cepanje papira kada se previse zvrlja po njemu.", UlogaId = programer.UlogaId, IgraId = igra2.IgraId },
                        new Zadatak { Naziv = "Prepreke prirode", Opis = "Dizajnirati scenu kako okretanje papira ne bi uticalo na utisak.", UlogaId = dizajner.UlogaId, IgraId = igra2.IgraId }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}