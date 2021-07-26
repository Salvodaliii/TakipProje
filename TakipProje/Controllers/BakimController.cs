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
    public class BakimController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AciklamaEkle(int id, string commenttext)
        {
            if (ModelState.IsValid)
            {
                Bakim bakim = db.Bakim.FirstOrDefault(x => x.ID == id);

                BakimAciklama Aciklama = new BakimAciklama()
                {
                    BakimID = bakim.ID,
                    Tarih = DateTime.Now,
                    Aciklama = commenttext
                };

                db.BakimAciklama.Add(Aciklama);
                db.SaveChanges();
            }

            return Redirect($"/Bakim/Details/{id}");
        }

        public ActionResult BakimPeriyotGuncelle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakim bakim = db.Bakim.Find(id);
            if (bakim == null)
            {
                return HttpNotFound();
            }

            return View(bakim);
        }



        [HttpGet]
        public JsonResult TestFunction2(int? ID) //Bakım/Create sayfasındaki combobox değerini ajax ile getiriyor ve PeriyotGun değişkenine atıyor.
        {

            Bakim bakim = db.Bakim.Find(ID);

            return Json(new
            {
                resut = "OK"
            });

        }











        [HttpPost]
        public JsonResult TestFunction2(Bakim bakim, int ID, string BakimAdi, DateTime BakimTarihi, int PeriyotGun, string Personel) //Bakım/Create sayfasındaki combobox değerini ajax ile getiriyor ve PeriyotGun değişkenine atıyor.
        {
            bakim.BakimAdi = BakimAdi;
            bakim.Periyot = PeriyotGun;
            bakim.BakimYapanPersonel = Personel;
            bakim.BakimTarihi = BakimTarihi;

            db.Entry(bakim).State = EntityState.Modified;
            db.SaveChanges();
            RedirectToAction("Index", "Bakim");


            return Json(new
            {
                resut = "OK"
            });

        }






        // GET: Bakim
        public ActionResult Index()
        {
            LisansIdCount();
            BakimIdCount();
            YedeklemeIdCount();
            ServisIdCount();
            return View(db.Bakim.ToList());
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

        // GET: Bakim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakim bakim = db.Bakim.Find(id);
            if (bakim == null)
            {
                return HttpNotFound();
            }
            return View(bakim);
        }

        // GET: Bakim/Create
        public ActionResult Create()
        {
            return View();
        }


        //
        [HttpPost]
        public JsonResult TestFunction(Bakim bakim, string BakimAdi,DateTime BakimTarihi,int PeriyotGun,string Personel) //Bakım/Create sayfasındaki combobox değerini ajax ile getiriyor ve PeriyotGun değişkenine atıyor.
        {
            bakim.BakimAdi = BakimAdi;
            bakim.Periyot = PeriyotGun;
            bakim.BakimYapanPersonel = Personel;
            bakim.BakimTarihi = BakimTarihi;
            db.Bakim.Add(bakim);
            db.SaveChanges();
            RedirectToAction("Index", "Bakim");


            return Json(new
            {
                resut = "OK"
            });

        }


        //


        // POST: Bakim/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BakimAdi,BakimTarihi,BakimYapanPersonel,Periyot")] Bakim bakim)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Bakim.Add(bakim);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            RedirectToAction("Index", "Bakim");
            return View(bakim);
            
        }

        // GET: Bakim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakim bakim = db.Bakim.Find(id);
            if (bakim == null)
            {
                return HttpNotFound();
            }
            return View(bakim);
        }

        // POST: Bakim/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BakimAdi,BakimTarihi,BakimYapanPersonel,Periyot")] Bakim bakim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bakim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bakim);
        }

        // GET: Bakim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakim bakim = db.Bakim.Find(id);
            if (bakim == null)
            {
                return HttpNotFound();
            }
            return View(bakim);
        }

        // POST: Bakim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bakim bakim = db.Bakim.Find(id);
            db.Bakim.Remove(bakim);
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
