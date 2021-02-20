using ETicaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret
{
    public class Sepet
    {
        public static Sepet AktifSepet
        {
            get
            {
                HttpContext ctx = HttpContext.Current; //online kullanıcı tutar
                if (ctx.Session["AktifSepet"] == null)
                    ctx.Session["AktifSepet"] = new Sepet();
                return (Sepet)ctx.Session["AktifSepet"];
            }
        }

        private List<SepetItem> urunler = new List<SepetItem>();

        public List<SepetItem> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }

        public void SepeteEkle(SepetItem spt)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Current.Session["AktifSepet"];

                if (s.Urunler.Any(i => i.Urun.Id == spt.Urun.Id))
                {
                    s.Urunler.FirstOrDefault(i => i.Urun.Id == spt.Urun.Id).Adet++;
                }
                else
                {
                    s.Urunler.Add(spt);
                }
            }
            else
            {
                Sepet s = new Sepet();
                s.Urunler.Add(spt);
                HttpContext.Current.Session["AktifSepet"] = s;
            }
        }

        public decimal ToplamTutar
        {
            get
            {
                return Urunler.Sum(i => i.ToplamFiyat);               
            }
        }
    }

    public class SepetItem
    {
        public Urun Urun { get; set; }
        public int Adet { get; set; }
        public double Indirim { get; set; }
        public decimal ToplamFiyat
        {
            get
            {
                return Urun.SatisFiyat * Adet * (decimal)(1 - Indirim);
            }
        }
    }
}