using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TakipProje.Models;

namespace TakipProje.Controllers
{
    public class GirisController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        public ActionResult UyeGiris() //üye giriş sayfası
        {

            return View();

        }

        [HttpPost]
        public ActionResult UyeGiris(Kullanicilar u)
        {

            var bilgiler = db.Kullanicilar.FirstOrDefault(x => x.KullaniciMail == u.KullaniciMail && x.KullaniciSifre == u.KullaniciSifre); //kullanıcı adı ve şifre var mı?            

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.ID.ToString(), false); //çerez oluşturulur.
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }
        }




        public ActionResult Logout() //kullanıcı çıkış yapar.
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UyeGiris", "Giris");
        }


        public ActionResult Profil() // profil sayfasına üyenin bilgileri yazar.
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var uye = db.Kullanicilar.Where(x => x.ID == id).ToList();
            List<Kullanicilar> tablo = db.Kullanicilar.Where(x => x.ID == id).ToList();
            var degerler = tablo.ToList();
            return View(degerler);
        }

    }
}