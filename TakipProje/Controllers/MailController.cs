using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProje.Models;
using TakipProje.Models.OtomatikMail;

namespace TakipProje.Controllers
{
    public class MailController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        public ActionResult LisansMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LisansMail([Bind(Include = "ID,LisansMail")] LisansMailPeriyodu lp, int LmailGun)
        {
            if (ModelState.IsValid)
            {
                lp.LisansMail = LmailGun;
                db.LisansMailPeriyodu.Add(lp);
                db.SaveChanges();
                return RedirectToAction("LisansMail");
            }
            return View(lp);
        }





        public ActionResult BakimMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BakimMail([Bind(Include = "ID,BakimMail")] BakimMailPeriyodu mp, int BmailGun)
        {
            if (ModelState.IsValid)
            {
                mp.BakimMail = BmailGun;
                db.BakimMailPeriyodu.Add(mp);
                db.SaveChanges();
                return RedirectToAction("BakimMail");
            }
            return View(mp);
        }





        public ActionResult YedeklemeMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YedeklemeMail([Bind(Include = "ID,YedeklemeMail")] YedeklemeMailPeriyodu yp, int YmailGun)
        {
            if (ModelState.IsValid)
            {
                yp.YedeklemeMail = YmailGun;
                db.YedeklemeMailPeriyodu.Add(yp);
                db.SaveChanges();
                return RedirectToAction("BakimMail");
            }
            return View(yp);
        }




    }
}