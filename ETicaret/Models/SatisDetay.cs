using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class SatisDetay : BaseHome
    {
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public float Indirim { get; set; }

        public int SatisId { get; set; }
        public int UrunId { get; set; }

        public virtual Satis Satis { get; set; }
        public virtual Urun Urun { get; set; }
    }
}