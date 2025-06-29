using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MarkoKosticIT6922.Models
{
    public class Korisnik : IdentityUser
    {
        [MaxLength(60)]
        required public string Ime { get; set; }

        public int UlogaId { get; set; }
        public Uloga? Uloga { get; set; }

        public bool? Admin { get; set; } = false;

        public ICollection<Resenje> Resenja { get; set; } = new List<Resenje>();
        public ICollection<Greska> Greske { get; set; } = new List<Greska>();
    }
}
