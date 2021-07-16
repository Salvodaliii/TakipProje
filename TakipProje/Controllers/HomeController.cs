using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProje.Models;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private takipDbEntities db = new takipDbEntities();

        //

        [HttpPost]
        public ActionResult Index(Email model, HttpPostedFileBase myFiles)
        {
            MailMessage mailim = new MailMessage();
            mailim.To.Add("yaz1100@outlook.com"); //Mesajın Gönderileceği Mail Adresi
            mailim.From = new MailAddress("lisansBitisTarihi@outlook.com"); //Hangi Mail Adresinden Gönderilecek ?



            string bbaslik = model.Baslik;
            bbaslik = "Başlık";

            string aadsoyad = model.AdSoyad;
            aadsoyad = "Uyarı Maili";

            string iicerik = model.Icerik;
            iicerik = "ŞUKADAR GÜN KALDI LİSANSIN BİTMESİNE !";


            mailim.Subject = "Bize Ulaşın Sayfasından Mesajınız Var. " + bbaslik; //başlık değerinin içine atama yap
            mailim.Body = "Sayın yetkili, " + aadsoyad + " kişisinden gelen mesajın içeriği aşağıdaki gibidir. <br>" + iicerik;
            mailim.IsBodyHtml = true;

            if (myFiles != null)
            {
                mailim.Attachments.Add(new Attachment(myFiles.InputStream, myFiles.FileName));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("lisansBitisTarihi@outlook.com", "BitisTarihi00");
            smtp.Port = 587;
            smtp.Host = "smtp-mail.outlook.com";
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mailim);
                TempData["Message"] = "Mesajınız iletilmiştir. En kısa zamanda size geri dönüş sağlanacaktır.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
            }

            return View();

        }






        //
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