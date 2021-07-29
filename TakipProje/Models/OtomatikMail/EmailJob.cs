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

            DateTime bugunTarihi = DateTime.UtcNow;
            var lisansTablosu = db.Lisans.ToList();

            foreach (var m in lisansTablosu)
            {
                var tarih = m.BitisTarihi;
                TimeSpan dif = bugunTarihi - tarih;
                int a = Convert.ToInt32(dif.TotalDays);
                a = a * (-1);




                if (a <= 10)
                {
                    using (var message = new MailMessage("lisansBitisTarihi@outlook.com", "yaz1100@outlook.com"))
                    {
                        message.Subject = "Lisans Süresi Hatırlatıcısı";
                        message.Body = m.ProgramAdi + "  Adlı Lisans " + m.BitisTarihi.ToString("dd-MM-yyyy") + "  tarihinde bitiyor";
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

                //a = a * (-1);
                //if (a <= 0)
                //{
                //    using (var message = new MailMessage("lisansBitisTarihi@outlook.com", "yaz1100@outlook.com"))
                //    {
                //        message.Subject = "Uyarı! Lisans Süresi Bitti!";
                //        message.Body = m.ProgramAdi + "  Adlı Lisans " + m.BitisTarihi.ToString("dd-MM-yyyy") + "  tarihinde bitmiştir.";
                //        using (SmtpClient client = new SmtpClient
                //        {
                //            EnableSsl = true,
                //            Host = "smtp-mail.outlook.com",
                //            Port = 587,
                //            Credentials = new NetworkCredential("lisansBitisTarihi@outlook.com", "BitisTarihi00")
                //        })
                //        {
                //            client.Send(message);
                //        }
                //    }
                //}





                //unutma
                //ad ve bitiş tarihi değerleri için bir for listesi oluşturulacak ve bu liste yine bir for ile indise göre göre çağırılacak ve top atacak.

            }

        }


    }
}