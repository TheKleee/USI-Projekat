namespace MarkoKosticIT6922.Models
{
    public class Greska
    {
        public int GreskaId { get; set; }

        required public string Opis { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik? Korisnik { get; set; }

        public int ResenjeId { get; set; }
        public Resenje? Resenje { get; set; }
    }
}