using ETicaret.Data;
using ETicaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            ViewBag.AktifKullanici = HttpContext.Application["AktifKullanici"];
            ViewBag.ToplamZiyaretci = HttpContext.Application["ToplamZiyaretci"];

            return View();
        }

        public PartialViewResult MiniSepetWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
                return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
            else
                return PartialView();
        }

        public void SepeteEkle(int id)
        {
            SepetItem spt = new SepetItem();
            Urun u = _db.Uruns.FirstOrDefault(i => i.Id == id);

            spt.Urun = u;
            spt.Adet = 1;
            spt.Indirim = 0;

            Sepet s = new Sepet();
            s.SepeteEkle(spt);
        }

        public void SepetUrunAdetDusur(int id)
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Session["AktifSepet"];

                if (s.Urunler.FirstOrDefault(i => i.Urun.Id == id).Adet > 1)
                {
                    s.Urunler.FirstOrDefault(i => i.Urun.Id == id).Adet--;
                }
                else
                {
                    SepetItem spt = s.Urunler.FirstOrDefault(i => i.Urun.Id == id);
                    s.Urunler.Remove(spt);
                }
            }
        }

        public PartialViewResult Slider()
        {
            return PartialView();
        }

        public PartialViewResult YeniUrunler()
        {
            using (_db = new ApplicationDbContext())
            {
                var data = _db.Uruns.Include("UrunOzelliks").Include("Marka").Include("Kategori").Include("SatisDetays").Include("Resims").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
                return PartialView(data);
            }
        }

        public PartialViewResult Servisler()
        {
            return PartialView();
        }

        public PartialViewResult Modalar()
        {
            return PartialView();
        }

        public PartialViewResult Markalar()
        {
            using (_db = new ApplicationDbContext())
            {
                var data = _db.Markas.Include("Uruns").Include("Resim").Where(i => i.IsDeleted == false).OrderBy(i => i.Uruns.Count()).ToList();
                return PartialView(data);
            }
        }

        public ActionResult UrunDetay(int? id)
        {
            Urun u = _db.Uruns.FirstOrDefault(i => i.Id == id);

            List<UrunOzellik> uos = _db.UrunOzelliks.Where(i => i.UrunId == u.Id).ToList();
            Dictionary<string, List<OzellikDeger>> ozellik = new Dictionary<string, List<OzellikDeger>>();
            List<OzellikDeger> degers = new List<OzellikDeger>();

            foreach (UrunOzellik uo in uos)
            {
                OzellikTip ot = _db.OzellikTips.FirstOrDefault(i => i.Id == uo.OzellikTipId);

                bool feriha = true;
                foreach (var item in ozellik)
                {
                    if (item.Key != ot.Adi)
                        feriha = true;
                    else
                        feriha = false;
                }
                if (feriha)
                    degers = new List<OzellikDeger>();

                foreach (OzellikDeger deger in _db.OzellikDegers.Where(i => i.OzellikTipId == ot.Id).ToList())
                {
                    OzellikDeger od = _db.OzellikDegers.FirstOrDefault(i => i.OzellikTipId == ot.Id && i.Id == uo.OzellikDegerId);
                    if (!degers.Any(i => i.Id == od.Id))
                        degers.Add(od);
                }
                if (ozellik.Any(i => i.Key == ot.Adi))
                {
                    ozellik[ot.Adi] = degers;
                }
                else
                    ozellik.Add(ot.Adi, degers);
            }
            ViewBag.OzellikTipler = ozellik;

            return View(u);
        }

        public ActionResult BunlaradaBak()
        {
            using (_db = new ApplicationDbContext())
            {
                var data = _db.Uruns.Include("UrunOzelliks").Include("Marka").Include("Kategori").Include("SatisDetays").Include("Resims").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(4).ToList();
                return PartialView(data);
            }
        }

        public ActionResult CookieAta()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CookieAta(string CookieAdi, string CookieDegeri)
        {
            HttpCookie ck = new HttpCookie(CookieAdi);
            ck.Value = CookieDegeri;
            ck.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(ck);
            return View();
        }

        public ActionResult CookieOku()
        {
            string deger = Request.Cookies["ocL"].Value;
            ViewBag.Cerez = deger;
            return View();
        }
    }
}