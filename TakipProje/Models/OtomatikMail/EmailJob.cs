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



            string ad2 = null;
            DateTime tarih2 = lisans.AlimTarihi;//başlangıç değeri olması amacıyla eklendi , bir işlevi yok.


            int[] fordegerleri = new int[60];


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

                if(kalangun <= 10)
                {
                    w++; // kaç tane <=10 kayıt var ?

                    tarihkayit[r] = tarih;
                    r++;

                    adkayit[j] = ad;
                    j++;

                    kalangunkayit[f] = kalangun;
                    f++;

                    mailatilacak = true;
                }

                


                //for (int i = 0; i < 60; i++)
                //{

                //    if (adkayit[i] != ad) //BURAYA HERTÜRLÜ GİRİYOR, NEDEN ?
                //    {
                //        fordegerleri[i] = i;

                //        adkayit[i] = ad;
                //    }
                //    else
                //    {
                //        i++;
                //    }

                //}
            }



                if (mailatilacak == true)
                {
                    using (var message = new MailMessage("lisansBitisTarihi@outlook.com", "takipproje@outlook.com"))
                    {

                    message.Subject = w + " Adet Lisansın Süresi Bitmek Üzere!!!!!!";

                    for (int k = 0; k < w; k++)
                    {
                        message.Body += "# " + adkayit[k] +" Adlı Lisansın Bitmesine " + kalangunkayit[k] + " Gün Kaldı. " + " Lisans Bitiş Tarihi : " + tarihkayit[k].ToString("dd-MM-yyyy") + " \n \r";
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