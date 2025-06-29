using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MarkoKosticIT6922.Models
{
    [Index(nameof(Naziv), IsUnique = true)]
    public class Igra
    {
        public int IgraId { get; set; }

        [MaxLength(60)]
        required public string Naziv { get; set; }
        public string? Opis { get; set; }

        public ICollection<Zadatak> Zadaci { get; set; } = new List<Zadatak>();
    }
}