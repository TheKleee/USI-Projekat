using MarkoKosticIT6922.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarkoKosticIT6922.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Igra> Igre { get; set; }
        public DbSet<Uloga> Uloge { get; set; }
        public DbSet<Zadatak> Zadaci { get; set; }
        public DbSet<Resenje> Resenja { get; set; }
        public DbSet<Greska> Greske { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Igra>().Property(i => i.Naziv).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Uloga>().Property(u => u.Naziv).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Korisnik>().Property(k => k.Ime).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Zadatak>().Property(z => z.Naziv).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Resenje>().Property(r => r.Opis).IsRequired();
            modelBuilder.Entity<Greska>().Property(g => g.Opis).IsRequired();
        }
    }
}
