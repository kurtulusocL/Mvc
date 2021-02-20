using ETicaret.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ETicaret.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base("DefaultConnection")
        {

        }

        public DbSet<Kargo> Kargoes { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Marka> Markas { get; set; }
        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<MusteriAdres> MusteriAdreses { get; set; }
        public DbSet<OzellikDeger> OzellikDegers { get; set; }
        public DbSet<OzellikTip> OzellikTips { get; set; }
        public DbSet<Resim> Resims { get; set; }
        public DbSet<Satis> Satises { get; set; }
        public DbSet<SatisDetay> SatisDetays { get; set; }
        public DbSet<SiparisDurum> SiparisDurums { get; set; }
        public DbSet<Urun> Uruns { get; set; }
        public DbSet<UrunOzellik> UrunOzelliks { get; set; }
    }
}