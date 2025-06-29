namespace MarkoKosticIT6922.Models
{
    public class Greska
    {
        public int GreskaId { get; set; }

        required public string Opis { get; set; }

        public string KorisnikId { get; set; } = String.Empty;
        public Korisnik? Korisnik { get; set; }

        public int ResenjeId { get; set; }
        public Resenje? Resenje { get; set; }
    }
}