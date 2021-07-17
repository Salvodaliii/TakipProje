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
    [Authorize]
    public class ServisController : Controller
    {
        private takipDbEntities db = new takipDbEntities();


        // GET: Servis
        public ActionResult Index()
        {
            LisansIdCount();
            BakimIdCount();
            YedeklemeIdCount();
            ServisIdCount();
            return View(db.Servis.ToList());
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





        // GET: Servis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            return View(servis);
        }

        // GET: Servis/Create
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult TFunction(Servis servis,int PeriyotGun, string isAdi, string FirmaAdi, string SabitTelefon, string DestekPersonelAdSoyad,string Gsm,string Mail,DateTime BaslangicTarihi,DateTime BitisTarihi,string BakimVeAciklamalar) //Servis/Create sayfasındaki combobox değerini ajax ile getiriyor ve PeriyotGun değişkenine atıyor.
        {
            servis.İsAdi = isAdi;
            servis.FirmaAdi = FirmaAdi;
            servis.SabitTelefon = SabitTelefon;
            servis.DestekPersonelAdSoyad = DestekPersonelAdSoyad;
            servis.Gsm = Gsm;
            servis.Mail = Mail;
            servis.BaslangicTarihi = BaslangicTarihi;
            servis.BitisTarihi = BitisTarihi;
            servis.BakimveAciklamalar = BakimVeAciklamalar;

            servis.BakimPeriyodu = PeriyotGun;

            db.Servis.Add(servis);
            db.SaveChanges();


            RedirectToAction("Index", "Servis");
            return Json(new
            {
                resut = "OK"
            });
        }


        // POST: Servis/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,İsAdi,BakimPeriyodu,FirmaAdi,SabitTelefon,DestekPersonelAdSoyad,Gsm,Mail,BaslangicTarihi,BitisTarihi,BakimveAciklamalar")] Servis servis)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Servis.Add(servis);
            //    db.SaveChanges();
                
            RedirectToAction("Index");
            return View(servis);
            //}


        }

        // GET: Servis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            return View(servis);
        }

        // POST: Servis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,İsAdi,BakimPeriyodu,FirmaAdi,SabitTelefon,DestekPersonelAdSoyad,Gsm,Mail,BaslangicTarihi,BitisTarihi,BakimveAciklamalar")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servis);
        }

        // GET: Servis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            return View(servis);
        }

        // POST: Servis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servis servis = db.Servis.Find(id);
            db.Servis.Remove(servis);
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
