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
    public class LisansDetayController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        // GET: LisansDetay
        public ActionResult Index(string p)
        {
            var degerler = from d in db.LisansDetay select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.ProgramAd.Contains(p));
            }
            return View(degerler.ToList());
        }

        // GET: LisansDetay/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LisansDetay lisansDetay = db.LisansDetay.Find(id);
            if (lisansDetay == null)
            {
                return HttpNotFound();
            }
            return View(lisansDetay);
        }

        // GET: LisansDetay/Create
        public ActionResult Create()
        {
            ViewBag.LisansID = new SelectList(db.Lisans, "ID", "ProgramAdi");
            return View();
        }

        // POST: LisansDetay/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProgramAd,ProgramTarih,ProgramFiyat")] LisansDetay lisansDetay)
        {
            if (ModelState.IsValid)
            {
                db.LisansDetay.Add(lisansDetay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lisansDetay);
        }

        // GET: LisansDetay/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LisansDetay lisansDetay = db.LisansDetay.Find(id);
            if (lisansDetay == null)
            {
                return HttpNotFound();
            }
            return View(lisansDetay);
        }

        // POST: LisansDetay/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProgramAd,ProgramTarih,ProgramFiyat")] LisansDetay lisansDetay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lisansDetay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lisansDetay);
        }

        // GET: LisansDetay/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LisansDetay lisansDetay = db.LisansDetay.Find(id);
            if (lisansDetay == null)
            {
                return HttpNotFound();
            }
            return View(lisansDetay);
        }

        // POST: LisansDetay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LisansDetay lisansDetay = db.LisansDetay.Find(id);
            db.LisansDetay.Remove(lisansDetay);
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
