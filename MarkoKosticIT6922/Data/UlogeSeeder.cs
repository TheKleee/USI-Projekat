
using MarkoKosticIT6922.Models;

namespace MarkoKosticIT6922.Data
{
    internal class UlogeSeeder
    {
        internal static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Uloge.Any())
            {
                context.Uloge.AddRange(
                    new Uloga { Naziv = "Programer" },
                    new Uloga { Naziv = "Dizajner" },
                    new Uloga { Naziv = "Tester" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}