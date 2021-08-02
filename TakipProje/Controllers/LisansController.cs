using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TakipProje.Models;
using System.Data.SqlClient;

namespace TakipProje.Controllers
{
    [Authorize]
    public class LisansController : Controller
    {
        private takipDbEntities db = new takipDbEntities();



        // GET: Lisans

        public ActionResult Index()
        {
            LisansIdCount();
            BakimIdCount();
            YedeklemeIdCount();
            ServisIdCount();
            return View(db.Lisans.ToList());
        }

        // GET: Lisans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lisans lisans = db.Lisans.Find(id);
            if (lisans == null)
            {
                return HttpNotFound();
            }
            return View(lisans);
        }

        // GET: Lisans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lisans/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProgramAdi,Adet,FirmaAdi,SabitTelefon,Gsm,Mail,AlimTarihi,BitisTarihi,YenilemeTarihi,AlisFiyati")] Lisans lisans, LisansDetay detay)
        {
            
            if (ModelState.IsValid)
            {
                lisans.YenilemeTarihi = lisans.AlimTarihi;
                LisansDetay lisansDetay = new LisansDetay();
                lisansDetay.ProgramAd = lisans.ProgramAdi;
                lisansDetay.ProgramTarih = lisans.AlimTarihi;
                lisansDetay.ProgramFiyat = lisans.AlisFiyati;
                lisansDetay.ProgramBitisTarihi = lisans.BitisTarihi;
                
                if(lisans.AlimTarihi < lisans.BitisTarihi)
                {
                    db.LisansDetay.Add(lisansDetay);
                    db.Lisans.Add(lisans);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(nameof(Lisans.BitisTarihi), "Bitiş Tarihi Alım Tarihinden Önce Olamaz !");
                }
            }

            return View(lisans);
        }

        // GET: Lisans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lisans lisans = db.Lisans.Find(id);
            if (lisans == null)
            {
                return HttpNotFound();
            }
            return View(lisans);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProgramAdi,Adet,FirmaAdi,SabitTelefon,Gsm,Mail,AlimTarihi,BitisTarihi,YenilemeTarihi,AlisFiyati")] Lisans lisans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lisans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lisans);
        }


        public ActionResult TarihDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lisans lisans = db.Lisans.Find(id);
            if (lisans == null)
            {
                return HttpNotFound();
            }
            return View(lisans);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TarihDuzenle([Bind(Include = "ID,ProgramAdi,Adet,FirmaAdi,SabitTelefon,Gsm,Mail,AlimTarihi,BitisTarihi,YenilemeTarihi,AlisFiyati")] Lisans lisans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lisans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lisans);
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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


    }

}
