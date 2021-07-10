using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TakipProje.Models;

namespace TakipProje.Controllers
{
    public class GuvenlikController : Controller
    {
        // GET: Guvenlik

        private takipDbEntities db = new takipDbEntities();

        public ActionResult GirisYap()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult GirisYap(Kullanicilar t)
        {
            var bilgiler = db.Kullanicilar.FirstOrDefault(x => x.KullaniciMail == t.KullaniciMail && x.KullaniciSifre == t.KullaniciSifre);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciMail, false);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError(nameof(Kullanicilar.KullaniciMail), "Kullanıcı Maili Yada Şifre Hatalı !");
                return View();
            }
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap","Guvenlik");
        }
    }

   
}