using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProje.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        public ActionResult Index()
        {
            LisanssIdCount();
            BakimIdCount();
            YedeklemeIdCount();
            ServisIdCount();
            return View();
        }


        public void LisanssIdCount()
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

        public void LisansIdCount()
        {
            ViewBag.displayclient = db.Lisans.ToList();
            ViewBag.Count = db.Lisans.Count();
        }

    }
}