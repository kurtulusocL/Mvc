using ETicaret.App_Classes;
using ETicaret.Data;
using ETicaret.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _db;

        public AdminController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Urunler(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var urunler = _db.Uruns.Include("UrunOzelliks").Include("Resims").Include("SatisDetays").Include("Kategori").Include("Marka").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25);
                return View(urunler);
            }
        }

        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = _db.Kategoris.Where(i => i.IsDeleted == false).OrderBy(i => i.Adi).ToList();
            ViewBag.Markalar = _db.Markas.Where(i => i.IsDeleted == false).OrderBy(i => i.Adi).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunEkle(Urun model)
        {
            if (ModelState.IsValid)
            {
                _db.Uruns.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("Urunler");
        }

        public ActionResult Markalar(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var marka = _db.Markas.Include("Uruns").Include("Resim").Where(i => i.IsDeleted == false).OrderBy(i => i.Uruns.Count()).ToPagedList(page, 15);
                return View(marka);
            }
        }

        public ActionResult MarkaEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkaEkle(Marka mrk, HttpPostedFileBase fileUpload)
        {
            int resimID = 0;
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaWidth"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaHeight"].ToString());

                string name = Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath("~/img/" + name));

                Resim rsm = new Resim();
                rsm.OrtaYol = name;
                _db.Resims.Add(rsm);
                _db.SaveChanges();

                //if (rsm.Id != null)
                resimID = rsm.Id;
            }
            if (resimID != -1)
                mrk.ResimId = resimID;

            _db.Markas.Add(mrk);
            _db.SaveChanges();

            return RedirectToAction("Markalar");
        }

        public ActionResult Kategoriler(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var cate = _db.Kategoris.Include("OzellikTips").Include("Uruns").Where(i => i.IsDeleted == false).OrderBy(i => i.Uruns.Count()).ToPagedList(page, 20);
                return View(cate);
            }
        }

        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriEkle(Kategori model)
        {
            if (ModelState.IsValid)
            {
                _db.Kategoris.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("Kategoriler");
        }

        public ActionResult OzellikTipleri(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var tip = _db.OzellikTips.Include("Kategori").Include("OzellikDegers").Include("UrunOzelliks").Where(i => i.IsDeleted == false).OrderBy(i => i.UrunOzelliks.Count()).ToPagedList(page, 25);
                return View(tip);
            }
        }

        public ActionResult OzellikTipEkle()
        {
            ViewBag.Kategoriler = _db.Kategoris.Where(i => i.IsDeleted == false).OrderBy(i => i.Adi).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OzellikTipEkle(OzellikTip model)
        {
            if (ModelState.IsValid)
            {
                _db.OzellikTips.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("OzellikTipleri");
        }

        public ActionResult OzellikDegerleri(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var deger = _db.OzellikDegers.Include("OzellikTip").Include("UrunOzelliks").Where(i => i.IsDeleted == false).OrderBy(i => i.UrunOzelliks.Count()).ToPagedList(page, 25);
                return View(deger);
            }
        }

        public ActionResult OzellikDegerEkle()
        {
            ViewBag.OzellikTipler = _db.OzellikTips.Where(i => i.IsDeleted == false).OrderBy(i => i.Adi).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OzellikDegerEkle(OzellikDeger model)
        {
            if (ModelState.IsValid)
            {
                _db.OzellikDegers.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("OzellikDegerleri");
        }

        public ActionResult UrunOzellikleri(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var ozellik = _db.UrunOzelliks.Include("OzellikTip").Include("Urun").Include("OzellikDeger").Where(i => i.IsDeleted == false).OrderBy(i => i.Urun.Adi).ToPagedList(page, 35);
                return View(ozellik);
            }
        }

        public ActionResult UrunOzellikEkle()
        {
            using (_db = new ApplicationDbContext())
            {
                var data = _db.Uruns.ToList();
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunOzellikEkle(UrunOzellik model)
        {
            if (ModelState.IsValid)
            {
                _db.UrunOzelliks.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("UrunOzellikleri");
        }

        public PartialViewResult UrunOzellikTipWidget(int? katId)
        {
            if (katId != null)
            {
                var data = _db.OzellikTips.Where(i => i.KategoriId == katId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = _db.OzellikTips.Where(i => i.IsDeleted == false).ToList();
                return PartialView(data);
            }
        }

        public PartialViewResult UrunOzellikDegerWidget(int? tipId)
        {
            if (tipId != null)
            {
                var data = _db.OzellikDegers.Where(i => i.OzellikTipId == tipId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = _db.OzellikDegers.Where(i => i.IsDeleted == false).ToList();
                return PartialView(data);
            }
        }

        public ActionResult UrunOzellikSil(int urunId, int tipId, int degerId)
        {
            UrunOzellik uo = _db.UrunOzelliks.FirstOrDefault(i => i.UrunId == urunId && i.OzellikTipId == tipId && i.OzellikDegerId == degerId);
            _db.UrunOzelliks.Remove(uo);
            _db.SaveChanges();

            return RedirectToAction("UrunOzellikleri");
        }

        public ActionResult UrunResimEkle(int? id)
        {
            var data = _db.Resims.Include("Urun").Include("Markas").FirstOrDefault(i => i.UrunId == id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunResimEkle(int? uId, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap ortaResim = new Bitmap(img, Settings.UrunOrtaBoyut);
                Bitmap buyukResim = new Bitmap(img, Settings.UrunBuyukBoyut);

                string ortaYol = "~/img/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string buyukYol = "~/img/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                ortaResim.Save(Server.MapPath(ortaYol));
                buyukResim.Save(Server.MapPath(buyukYol));

                Resim rsm = new Resim();
                rsm.BuyukYol = buyukYol;
                rsm.OrtaYol = ortaYol;
                rsm.UrunId = uId;

                if (_db.Resims.FirstOrDefault(i => i.UrunId == uId && i.Varsayılan == false) != null)
                    rsm.Varsayılan = true;
                else
                    rsm.Varsayılan = false;

                _db.Resims.Add(rsm);
                _db.Entry(rsm).State = EntityState.Added;
                _db.SaveChanges();

                return View(uId);
                //return RedirectToAction("UrunResimEkle", new { id = uId });
            }
            return View(uId);
            //return RedirectToAction("UrunResimEkle", new { id = uId });
        }
    }
}