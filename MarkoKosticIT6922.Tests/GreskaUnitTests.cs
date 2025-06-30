using MarkoKosticIT6922.Models;
using Xunit;

namespace MarkoKosticIT6922.Tests
{
    public class GreskaUnitTests
    {
        [Fact]
        public void MozeNapravitiGresku_SaOpisom()
        {
            var greska = new Greska { Opis = "Test greska" };

            Assert.NotNull(greska);
            Assert.Equal("Test greska", greska.Opis);
        }

        [Fact]
        public void ProveraGreska_InicijalneVrednosti()
        {
            var greska = new Greska
            {
                Opis = "Neka greska",
                KorisnikId = "user1",
                ResenjeId = 5
            };

            Assert.Equal("Neka greska", greska.Opis);
            Assert.Equal("user1", greska.KorisnikId);
            Assert.Equal(5, greska.ResenjeId);
        }
    }
}
