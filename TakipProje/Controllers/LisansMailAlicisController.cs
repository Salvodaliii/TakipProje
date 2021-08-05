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
    public class LisansMailAlicisController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        public ActionResult Index()
        {
            return View(db.LisansMailAlici.ToList());
        }

        // GET: LisansMailAlicis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LisansMailAlici lisansMailAlici = db.LisansMailAlici.Find(id);
            if (lisansMailAlici == null)
            {
                return HttpNotFound();
            }
            return View(lisansMailAlici);
        }

        // GET: LisansMailAlicis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LisansMailAlicis/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AliciMailAdresi")] LisansMailAlici lisansMailAlici)
        {
            if (ModelState.IsValid)
            {
                db.LisansMailAlici.Add(lisansMailAlici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lisansMailAlici);
        }

        // GET: LisansMailAlicis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LisansMailAlici lisansMailAlici = db.LisansMailAlici.Find(id);
            if (lisansMailAlici == null)
            {
                return HttpNotFound();
            }
            return View(lisansMailAlici);
        }

        // POST: LisansMailAlicis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,AliciMailAdresi")] LisansMailAlici lisansMailAlici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lisansMailAlici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lisansMailAlici);
        }

        // GET: LisansMailAlicis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LisansMailAlici lisansMailAlici = db.LisansMailAlici.Find(id);
            if (lisansMailAlici == null)
            {
                return HttpNotFound();
            }
            return View(lisansMailAlici);
        }

        // POST: LisansMailAlicis/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LisansMailAlici lisansMailAlici = db.LisansMailAlici.Find(id);
            db.LisansMailAlici.Remove(lisansMailAlici);
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
