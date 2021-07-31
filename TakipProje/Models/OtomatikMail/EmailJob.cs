﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace TakipProje.Models.OtomatikMail
{
    public class EmailJob : IJob
    {
        takipDbEntities db = new takipDbEntities();

        
        public void Execute(IJobExecutionContext context)
        {        

            Lisans lisans = new Lisans();

            DateTime bugunTarihi = DateTime.UtcNow;
            var lisansTablosu = db.Lisans.ToList();

            int kayitsayisi = db.Lisans.Count(); // lisans tablosunda kaç kayıt var ?

            string[] adkayit = new string[kayitsayisi];
            DateTime[] tarihkayit = new DateTime[kayitsayisi];
            int[] kalangunkayit = new int[kayitsayisi];

            string ad = null;
            DateTime tarih = lisans.AlimTarihi; //başlangıç değeri olması amacıyla eklendi , bir işlevi yok.
            int kalangun = 0;
            TimeSpan dif;

            //indisler için atamalar ;
            int j = 0; //adların sayısı
            int r = 0; //tarihlerin sayısı
            int f = 0; //kalangünlerin sayısı
            int w = 0; //<=10 günü kalan lisanslar

            bool mailatilacak = false; //en az bir lisans <=10 ise bu değer 1 olacak (true)

            foreach (var m in lisansTablosu)
            {
                ad = m.ProgramAdi;
                tarih = m.BitisTarihi;
                

                dif = bugunTarihi - tarih;
                kalangun = Convert.ToInt32(dif.TotalDays);
                kalangun *= (-1);

                if(kalangun <= 10)  //10 yerine kullanıcıdan bir sayı alınacak ve o kontrol edilecek. !önemli!
                {
                    w++; // kaç tane <=10 kayıt var ?

                    tarihkayit[r] = tarih;
                    r++; //tarih sayısı kadar indis oluşturmak için r değeri kullanıldı ve her tarih değeri geldiğinde 1 arttı.

                    adkayit[j] = ad;
                    j++;

                    kalangunkayit[f] = kalangun;
                    f++;  //kalangün sayısı kadar indis oluşturmak için f değeri kullanıldı ve her kalangün değeri hesaplandığında 1 arttı.

                    mailatilacak = true;  //eğer kalangün<=10 ise en az 1 lisansa ait mail atılacağından , mailatilacak değeri true olarak ayarlandı.
                }
            }


            if (mailatilacak == true) //kalangün <=10 koşulu sağlandı (en az 1 lisansın maili atılacak)
                {
                using (var message = new MailMessage("lisansBitisTarihi@outlook.com", "takipproje@outlook.com"))
                    {

                    message.Subject = w + " Adet Lisansın Süresi Bitmek Üzere";

                    for (int k = 0; k < w; k++)
                    {
                        if (kalangunkayit[k] <= 0) //Eğer lisans süresi bitmiş ise;
                        {
                           message.Body += "# " + adkayit[k] + " Adlı Lisansın Süresi " + (kalangunkayit[k]*-1) + " GÜN ÖNCE BİTTİ ! " + " Lisans Bitiş Tarihi : " + tarihkayit[k].ToString("dd-MM-yyyy") + " \n \r";

                        }

                        else //eğer lisans süresi bitmemiş fakat yaklaşıyor ise;
                        {
                            message.Body += "# " + adkayit[k] + " Adlı Lisansın Bitmesine " + kalangunkayit[k] + " Gün Kaldı. " + " Lisans Bitiş Tarihi : " + tarihkayit[k].ToString("dd-MM-yyyy") + " \n \r";

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