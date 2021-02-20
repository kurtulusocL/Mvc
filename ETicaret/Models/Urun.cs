using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class Urun : BaseHome
    {
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public decimal AlisFiyat { get; set; }
        public decimal SatisFiyat { get; set; }
        public DateTime SonKullanmaTarih { get; set; }

        public int KategoriId { get; set; }
        public int MarkaId { get; set; }

        public virtual Kategori Kategori { get; set; }
        public virtual Marka Marka { get; set; }

        public ICollection<UrunOzellik> UrunOzelliks { get; set; }
        public ICollection<SatisDetay> SatisDetays { get; set; }
        public ICollection<Resim> Resims { get; set; }
    }
}