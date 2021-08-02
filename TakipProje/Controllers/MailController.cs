using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProje.Models.OtomatikMail;

namespace TakipProje.Controllers
{
    public class MailController : Controller
    {
        public ActionResult LisansMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LisansMail(LisansEmailJob lJob, int LmailGun)
        {
            lJob.lisansMailAtmaGunu = LmailGun;
            return View(lJob);
        }





        public ActionResult BakimMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BakimMail(BakimEmailJob lJob, int BmailGun)
        {
            lJob.bakimMailAtmaGunu = BmailGun;
            return View(lJob);
        }





        public ActionResult YedeklemeMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YedeklemeMail(YedeklemeEmailJob lJob, int YmailGun)
        {
            lJob.yedeklemeMailAtmaGunu = YmailGun;
            return View(lJob);
        }




    }
}