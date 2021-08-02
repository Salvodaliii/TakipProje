using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace TakipProje.Models.OtomatikMail
{
    public class BakimEmailJob : IJob
    {

        takipDbEntities db = new takipDbEntities();

        public void Execute(IJobExecutionContext context)
        {

            Bakim bakim = new Bakim();

            DateTime bugunTarihi = DateTime.UtcNow;
            var bakimTablosu = db.Bakim.ToList();

            int kayitsayisi = db.Bakim.Count();

            string[] adkayit = new string[kayitsayisi];
            DateTime[] tarihkayit = new DateTime[kayitsayisi];
            int[] kalangunkayit = new int[kayitsayisi];

            string ad = null;

            int? periyot = 0;
            DateTime periyotartibugun = bakim.BakimTarihi;

            DateTime tarih = bakim.BakimTarihi; //başlangıç değeri olması amacıyla eklendi , bir işlevi yok.
            int kalangun = 0;
            TimeSpan dif;

            //indisler için atamalar ;
            int j = 0; //adların sayısı
            int r = 0; //tarihlerin sayısı
            int f = 0; //kalangünlerin sayısı
            int w = 0; //<=10 günü kalan lisanslar

            bool mailatilacak = false; //en az bir bakım <=10 ise bu değer 1 olacak (true)

            foreach (var m in bakimTablosu)
            {
                ad = m.BakimAdi;
                periyot = m.Periyot;
                periyotartibugun = DateTime.UtcNow;
                periyotartibugun = m.BakimTarihi.AddDays(Convert.ToDouble(periyot));

                
                tarih = periyotartibugun;


                dif = bugunTarihi - tarih;
                kalangun = Convert.ToInt32(dif.TotalDays);
                kalangun *= (-1);

                if (kalangun <= 10)  //10 yerine kullanıcıdan bir sayı alınacak ve o kontrol edilecek. !önemli!
                {
                    w++; // kaç tane <=10 kayıt var ?

                    tarihkayit[r] = tarih;
                    r++; //tarih sayısı kadar indis oluşturmak için r değeri kullanıldı ve her tarih değeri geldiğinde 1 arttı.

                    adkayit[j] = ad;
                    j++;

                    kalangunkayit[f] = kalangun;
                    f++;  //kalangün sayısı kadar indis oluşturmak için f değeri kullanıldı ve her kalangün değeri hesaplandığında 1 arttı.

                    mailatilacak = true;  //eğer kalangün<=10 ise en az 1 bakıma ait mail atılacağından , mailatilacak değeri true olarak ayarlandı.
                }
            }


            if (mailatilacak == true) //kalangün <=10 koşulu sağlandı (en az 1 bakımın maili atılacak)
            {
                using (var message = new MailMessage("lisansBitisTarihi@outlook.com", "takipproje@outlook.com"))
                {
                    message.IsBodyHtml = true; //HTML TAGLARINI KULLANMA İMKANI SAĞLAR.

                    message.Subject = " [Uyarı!] " + w + " Adet Bakım İle İlgili İşlem Yapılması Gerekiyor.";

                    for (int k = 0; k < w; k++)
                    {
                        if (kalangunkayit[k] < 0) //Eğer bakım süresi bitmiş ise;
                        {
                            message.Body += "<b><font color='crimson'># [BİTTİ] # </font></b> <b>[</b> " + adkayit[k] + " Adlı Bakımın Vakti "+ "<font color='crimson'>*" + (kalangunkayit[k] * -1)+ "GÜN ÖNCE GEÇTİ !* </font>" + " Bakım Bitiş Tarihi : " + tarihkayit[k].ToString("dd-MM-yyyy") + "<b> ]</b>" + "<br/><br/>";

                        }

                        if (kalangunkayit[k] == 0) //Eğer bakım süresi bugün bitiyorsa ise;
                        {
                            message.Body += "<b><font color='crimson'># [BİTTİ] # </font></b> <b>[</b> " + adkayit[k] + " Adlı Bakımın Vakti <font color='crimson'>*BUGÜN BİTTİ !*</font>" + " Bakım Bitiş Tarihi : " + tarihkayit[k].ToString("dd-MM-yyyy") + "<b> ]</b>" + "<br/><br/>";

                        }

                        if (kalangunkayit[k] > 0) //Eğer bakım süresi bitmemiş ise kaç gün kalmış ?
                        {
                            message.Body += "<b><font color='darkorange'># [YAKLAŞIYOR] # </font></b> <b>[</b> " + adkayit[k] + " Adlı Bakımın Vaktine " + "<font color='darkorange'>*" + kalangunkayit[k]+ " Gün Kaldı.*</font>" + " Bakım Bitiş Tarihi : " + tarihkayit[k].ToString("dd-MM-yyyy") + "<b> ]</b>" + "<br/><br/>";
                        }
                    }

                    using (SmtpClient client = new SmtpClient
                    {
                        EnableSsl = true,
                        Host = "smtp-mail.outlook.com",
                        Port = 587,
                        Credentials = new NetworkCredential("lisansBitisTarihi@outlook.com", "BitisTarihi00")
                    })

                    {
                        client.Send(message);
                    }

                }
            }


        }

    }
}