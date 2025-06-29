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

            modelBuilder.Entity<Greska>()
                .HasOne(g => g.Korisnik)
                .WithMany()
                .HasForeignKey(g => g.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Greska>()
                .HasOne(g => g.Resenje)
                .WithMany(r => r.Greske)
                .HasForeignKey(g => g.ResenjeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
