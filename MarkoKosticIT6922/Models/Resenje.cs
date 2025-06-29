namespace MarkoKosticIT6922.Models
{
    public class Resenje
    {
        public int ResenjeId { get; set; }

        required public string Opis { get; set; }

        public int ZadatakId { get; set; }
        public Zadatak? Zadatak { get; set; }

        public string KorisnikId { get; set; } = String.Empty;
        public Korisnik? Korisnik { get; set; }

        public bool Odobreno { get; set; } = false;

        public ICollection<Greska> Greske { get; set; } = new List<Greska>();
    }
}