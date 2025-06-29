
using MarkoKosticIT6922.Models;

namespace MarkoKosticIT6922.Data
{
    internal class IgreSeeder
    {
        internal static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Igre.Any())
            {
                context.Igre.AddRange(
                    new Igra { Naziv = "RPG Tower", Opis = "Borba kula sa rpg elementima!" },
                    new Igra { Naziv = "Crazy War", Opis = "Kreativna borba na papiru sa korisnicki programiranim magijama!" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}