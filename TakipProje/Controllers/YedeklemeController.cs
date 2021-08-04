using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TakipProje.Models;

namespace TakipProje.Controllers
{
    //[Authorize]
    public class YedeklemeController : Controller
    {
        private takipDbEntities db = new takipDbEntities();
        // GET: Yedekleme
        public ActionResult Index()
        {
            LisansIdCount();
            BakimIdCount();
            YedeklemeIdCount();
            ServisIdCount();
            return View(db.Yedekleme.ToList());
        }

        public void LisansIdCount()
        {
            ViewBag.displayclient = db.Lisans.ToList();
            ViewBag.LisansKayitSayisi = db.Lisans.Count();
        }

        public void BakimIdCount()
        {
            ViewBag.displayclient = db.Bakim.ToList();
            ViewBag.BakimKayitSayisi = db.Bakim.Count();
        }

        public void ServisIdCount()
        {
            ViewBag.displayclient = db.Servis.ToList();
            ViewBag.ServisKayitSayisi = db.Servis.Count();
        }

        public void YedeklemeIdCount()
        {
            ViewBag.displayclient = db.Yedekleme.ToList();
            ViewBag.YedeklemeKayitSayisi = db.Yedekleme.Count();
        }





        public ActionResult YedeklemePeriyotGuncelle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yedekleme yedekleme = db.Yedekleme.Find(id);
            if (yedekleme == null)
            {
                return HttpNotFound();
            }

            return View(yedekleme);
        }



        [HttpGet]
        public JsonResult TestFunction4(int? ID)
        {
            Yedekleme yedekleme = db.Yedekleme.Find(ID);

            return Json(new
            {
                resut = "OK"
            });

        }


        [HttpPost]
        public JsonResult TestFunction4(Yedekleme yedekleme, int PeriyotGun, int ID, string Adi, string Durum, DateTime OlusturmaTarihi, DateTime SonYedeklemeTarihi, String KontrolEdenPersonel)
        {
            yedekleme.YedeklemePlaniAdi = Adi;
            yedekleme.Durum = Durum;
            yedekleme.OlusturmaTarihi = OlusturmaTarihi;
            yedekleme.SonYedeklemeTarihi = SonYedeklemeTarihi;
            yedekleme.KontrolEdenPersonel = KontrolEdenPersonel;
            yedekleme.YedeklemePeriyodu = PeriyotGun;

            db.Entry(yedekleme).State = EntityState.Modified;
            db.SaveChanges();
            RedirectToAction("Index", "Yedekleme");


            return Json(new
            {
                resut = "OK"
            });

        }





        // GET: Yedekleme/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yedekleme yedekleme = db.Yedekleme.Find(id);
            if (yedekleme == null)
            {
                return HttpNotFound();
            }
            return View(yedekleme);
        }

        // GET: Yedekleme/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult TestFunction(Yedekleme yedekleme, int PeriyotGun, string YedeklemePlanAdi, string Durum, DateTime OlusturmaTarihi,DateTime SonYedeklemeTarihi, string KontrolEdenPersonel)
        {

            yedekleme.YedeklemePeriyodu = PeriyotGun;
            yedekleme.YedeklemePlaniAdi = YedeklemePlanAdi;
            yedekleme.Durum = Durum;
            yedekleme.OlusturmaTarihi = OlusturmaTarihi;
            yedekleme.SonYedeklemeTarihi = SonYedeklemeTarihi;
            yedekleme.KontrolEdenPersonel = KontrolEdenPersonel;

            db.Yedekleme.Add(yedekleme);
            db.SaveChanges();

            RedirectToAction("Index", "Yedekleme");
            return Json(new
            {
                resut = "OK"
            });
        }



        // POST: Yedekleme/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,YedeklemePeriyodu,YedeklemePlaniAdi,Durum,OlusturmaTarihi,SonYedeklemeTarihi,KontrolEdenPersonel")] Yedekleme yedekleme)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Yedekleme.Add(yedekleme);
            //    db.SaveChanges();
             RedirectToAction("Index");
            //}

            return View(yedekleme);
        }

        // GET: Yedekleme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yedekleme yedekleme = db.Yedekleme.Find(id);
            if (yedekleme == null)
            {
                return HttpNotFound();
            }
            return View(yedekleme);
        }

        // POST: Yedekleme/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,YedeklemePeriyodu,YedeklemePlaniAdi,Durum,OlusturmaTarihi,SonYedeklemeTarihi,KontrolEdenPersonel")] Yedekleme yedekleme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yedekleme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yedekleme);
        }

        // GET: Yedekleme/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yedekleme yedekleme = db.Yedekleme.Find(id);
            if (yedekleme == null)
            {
                return HttpNotFound();
            }
            return View(yedekleme);
        }

        // POST: Yedekleme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yedekleme yedekleme = db.Yedekleme.Find(id);
            db.Yedekleme.Remove(yedekleme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
