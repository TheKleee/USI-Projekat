using System.ComponentModel.DataAnnotations;

namespace MarkoKosticIT6922.Models
{
    public class Uloga
    {
        public int UlogaId { get; set; }

        [MaxLength(20)]
        required public string Naziv { get; set; }

        public ICollection<Korisnik> Korisnici { get; set; } = new List<Korisnik>();
    }
}
