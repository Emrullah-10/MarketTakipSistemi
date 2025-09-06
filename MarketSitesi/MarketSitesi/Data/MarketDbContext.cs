using Microsoft.EntityFrameworkCore;
using MarketSitesi.Models;

namespace MarketSitesi.Data
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
        {
        }

        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yiyecek> Yiyecekler { get; set; }
        public DbSet<Icecek> Icecekler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kategori - Yiyecek ilişkisi (One-to-Many)
            modelBuilder.Entity<Yiyecek>()
                .HasOne(y => y.Kategori)
                .WithMany(k => k.Yiyecekler)
                .HasForeignKey(y => y.KategoriId)
                .OnDelete(DeleteBehavior.Cascade);

            // Yiyecek - İçecek ilişkisi (One-to-Many)
            modelBuilder.Entity<Icecek>()
                .HasOne(i => i.Yiyecek)
                .WithMany(y => y.Icecekler)
                .HasForeignKey(i => i.YiyecekId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            modelBuilder.Entity<Kategori>().HasData(
                new Kategori { Id = 1, Ad = "Et ve Tavuk", Aciklama = "Et ve tavuk ürünleri" },
                new Kategori { Id = 2, Ad = "Süt Ürünleri", Aciklama = "Süt ve süt ürünleri" },
                new Kategori { Id = 3, Ad = "Meyve ve Sebze", Aciklama = "Taze meyve ve sebzeler" },
                new Kategori { Id = 4, Ad = "İçecekler", Aciklama = "Çeşitli içecekler" }
            );

            modelBuilder.Entity<Yiyecek>().HasData(
                new Yiyecek { Id = 1, Ad = "Tavuk Göğsü", Aciklama = "Taze tavuk göğsü", Fiyat = 45.50m, StokAdedi = 20, KategoriId = 1 },
                new Yiyecek { Id = 2, Ad = "Süt", Aciklama = "1 litre tam yağlı süt", Fiyat = 12.75m, StokAdedi = 50, KategoriId = 2 },
                new Yiyecek { Id = 3, Ad = "Elma", Aciklama = "Kırmızı elma", Fiyat = 8.90m, StokAdedi = 100, KategoriId = 3 }
            );

            modelBuilder.Entity<Icecek>().HasData(
                new Icecek { Id = 1, Ad = "Kola", Aciklama = "Gazlı içecek", Fiyat = 5.50m, StokAdedi = 200, Hacim = 330, YiyecekId = 1 },
                new Icecek { Id = 2, Ad = "Su", Aciklama = "Doğal kaynak suyu", Fiyat = 2.00m, StokAdedi = 500, Hacim = 500, YiyecekId = 2 }
            );
        }
    }
}
