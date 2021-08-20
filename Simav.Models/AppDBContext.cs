using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Simav.Models
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bilgilendirme> Bilgilendirmes { get; set; }
        public virtual DbSet<Duyuru> Duyurus { get; set; }
        public virtual DbSet<Etkinler> Etkinlers { get; set; }
        public virtual DbSet<Haberler> Haberlers { get; set; }
        public virtual DbSet<Ihaleler> Ihalelers { get; set; }
        public virtual DbSet<Ilanlar> Ilanlars { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }
        public virtual DbSet<MeclisGundem> MeclisGundems { get; set; }
        public virtual DbSet<Olumler> Olumlers { get; set; }
        public virtual DbSet<Personel> Personels { get; set; }
        public virtual DbSet<Projeler> Projelers { get; set; }
        public virtual DbSet<Sayfalar> Sayfalars { get; set; }
        public virtual DbSet<Sikayetler> Sikayetlers { get; set; }
        public virtual DbSet<Videolar> Videolars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bilgilendirme>(entity =>
            {
                entity.ToTable("Bilgilendirme");

                entity.Property(e => e.Ad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik).IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");
            });

            modelBuilder.Entity<Duyuru>(entity =>
            {
                entity.ToTable("Duyuru");

                entity.Property(e => e.Ad)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik).IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");
            });

            modelBuilder.Entity<Etkinler>(entity =>
            {
                entity.HasKey(e => e.EtkinlikId);

                entity.ToTable("Etkinler");

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.EtkinlikAdi).HasMaxLength(50);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Haberler>(entity =>
            {
                entity.ToTable("Haberler");

                entity.Property(e => e.Ad)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik).IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Resim).IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");
            });

            modelBuilder.Entity<Ihaleler>(entity =>
            {
                entity.HasKey(e => e.IhaleId);

                entity.ToTable("Ihaleler");

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");

                entity.Property(e => e.IhaleAdi).HasMaxLength(300);
            });

            modelBuilder.Entity<Ilanlar>(entity =>
            {
                entity.ToTable("Ilanlar");

                entity.Property(e => e.Ad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik).IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KısaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");
            });

            modelBuilder.Entity<Kullanicilar>(entity =>
            {
                entity.HasKey(e => e.KullaniciId);

                entity.ToTable("Kullanicilar");

                entity.Property(e => e.Ad)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KullaniciAdi)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Soyad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Unvan)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MeclisGundem>(entity =>
            {
                entity.HasKey(e => e.GundemId);

                entity.ToTable("MeclisGundem");

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.DosyaYolu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Olumler>(entity =>
            {
                entity.HasKey(e => e.OlumId);

                entity.ToTable("Olumler");

                entity.Property(e => e.OlumId).ValueGeneratedNever();

                entity.Property(e => e.AdSoyad)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Cami)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik).IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Mezarlik)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");
            });

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.ToTable("Personel");

                entity.HasComment("1=Başkan,2=Başkan Yardımcısı,3=Müdürler, 4=Meclis Üyesi");

                entity.Property(e => e.AdSoyad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cv)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fb)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.Resim)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tw)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Unvan)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Projeler>(entity =>
            {
                entity.HasKey(e => e.ProjeId);

                entity.ToTable("Projeler");

                entity.Property(e => e.Ad)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik).IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KısaAciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sayfalar>(entity =>
            {
                entity.HasKey(e => e.SayfaId);

                entity.ToTable("Sayfalar");

                entity.HasComment("Iletisim");

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.KisaAciklama).HasMaxLength(500);

                entity.Property(e => e.SayfaId).ValueGeneratedOnAdd();

                entity.Property(e => e.SayfaAdi).HasMaxLength(50);
            });

            modelBuilder.Entity<Sikayetler>(entity =>
            {
                entity.HasKey(e => e.SikayetId);

                entity.ToTable("Sikayetler");

                entity.Property(e => e.Ad)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Icerik).IsUnicode(false);

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IP");

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.Soyad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");
            });

            modelBuilder.Entity<Videolar>(entity =>
            {
                entity.HasKey(e => e.VideoId);

                entity.ToTable("Videolar");

                entity.Property(e => e.Aciklama)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Ad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DegistirmeTarihi).HasColumnType("datetime");

                entity.Property(e => e.Embed)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.KayıtTarihi).HasColumnType("datetime");

                entity.Property(e => e.Resim)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
