using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProje.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        public ActionResult Index()
        {
            BakimIdCount();
            LisansIdCount();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void LisansIdCount()
        {
            ViewBag.displayclient = db.Lisans.ToList();
            ViewBag.Count = db.Lisans.Count();

        }
        public void BakimIdCount()
        {
            ViewBag.displayclient = db.Bakim.ToList();
            ViewBag.Count = db.Bakim.Count();
        }

    }
}