using System.ComponentModel.DataAnnotations;

namespace MarkoKosticIT6922.Models
{
    public class Zadatak
    {
        public int ZadatakId { get; set; }

        [MaxLength(60)]
        required public string Naziv { get; set; }
        public string? Opis { get; set; }

        public int IgraId { get; set; }
        public Igra? Igra { get; set; }

        public int UlogaId { get; set; }
        public Uloga? Uloga { get; set; }

        public DateTime? Rok { get; set; }

        public bool? Reseno { get; set; } = false;

        public ICollection<Resenje> Resenja { get; set; } = new List<Resenje>();
        public ICollection<Greska> Greske { get; set; } = new List<Greska>();
    }
}