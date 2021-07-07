﻿using System;
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
    public class LisansController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        // GET: Lisans
        public ActionResult Index()
        {
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
        public ActionResult Create([Bind(Include = "ID,ProgramAdi,Adet,FirmaAdi,SabitTelefon,Gsm,Mail,AlimTarihi,BitisTarihi,YenilemeTarihi,AlisFiyati")] Lisans lisans)
        {
            if (ModelState.IsValid)
            {
                db.Lisans.Add(lisans);
                db.SaveChanges();
                return RedirectToAction("Index");
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

        // POST: Lisans/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProgramAdi,Adet,FirmaAdi,SabitTelefon,Gsm,Mail,AlimTarihi,BitisTarihi,YenilemeTarihi,AlisFiyati")] Lisans lisans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lisans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(lisans);
        }

        // GET: Lisans/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Lisans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lisans lisans = db.Lisans.Find(id);
            db.Lisans.Remove(lisans);
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
