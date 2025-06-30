using MarkoKosticIT6922.Controllers;
using MarkoKosticIT6922.Data;
using MarkoKosticIT6922.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkoKosticIT6922.Tests
{
    public class GreskaFeatureTests
    {
        private ApplicationDbContext DohvatiDbContextIzMemorije()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Kreiraj_Post_ValidnaGreska_PreusmeravaNaIndex()
        {
            var context = DohvatiDbContextIzMemorije();
            var controller = new GreskasController(context);

            var greska = new Greska
            {
                Opis = "Neki opis greske",
                KorisnikId = "user1",
                ResenjeId = 1
            };

            var result = await controller.Create(greska);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
