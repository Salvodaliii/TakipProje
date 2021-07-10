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
