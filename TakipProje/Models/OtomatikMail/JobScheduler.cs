using Quartz;
using Quartz.Impl;
using System;

namespace TakipProje.Models.OtomatikMail
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler lisansbildiri = StdSchedulerFactory.GetDefaultScheduler();
            lisansbildiri.Start(); //Lisans bildiri maili göndermek için ilk tanımlama yapılıyor.

            IScheduler bakimbildiri = StdSchedulerFactory.GetDefaultScheduler();
            bakimbildiri.Start(); //Bakım bildiri maili göndermek için ilk tanımlama yapılıyor.

            IScheduler yedeklemebildiri = StdSchedulerFactory.GetDefaultScheduler();
            yedeklemebildiri.Start(); //Yedekleme bildiri maili göndermek için ilk tanımlama yapılıyor.

            IJobDetail joblisans = JobBuilder.Create<LisansEmailJob>().Build(); //EmailJob-> Lisans mail gönderme kodlarını içeren sınıfına bağlanıyor.
            IJobDetail jobbakim = JobBuilder.Create<BakimEmailJob>().Build(); //Bakım mail gönderme kodlarını içeren sınıfa bağlanıyor.
            IJobDetail jobyedekleme = JobBuilder.Create<YedeklemeEmailJob>().Build();//Yedekleme mail gönderme kodlarını içeren sınıfa bağlanıyor.

            ITrigger lisanstetikle = TriggerBuilder.Create().WithDailyTimeIntervalSchedule
            (L =>L.WithIntervalInSeconds(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))).Build(); //Lisans mail gönderimini belirlenen süre ile tetikle.

            ITrigger bakimtetikle = TriggerBuilder.Create().WithDailyTimeIntervalSchedule
            (B => B.WithIntervalInSeconds(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))).Build();//Bakım mail gönderimini belirlenen süre ile tetikle.

            ITrigger yedeklemetetikle = TriggerBuilder.Create().WithDailyTimeIntervalSchedule
            (Y => Y.WithIntervalInSeconds(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))).Build();//Bakım mail gönderimini belirlenen süre ile tetikle.

            lisansbildiri.ScheduleJob(joblisans, lisanstetikle); //lisans bildiri mailini göndermek için lisansjob'u tetikle.
            bakimbildiri.ScheduleJob(jobbakim, bakimtetikle);//bakım bildiri mailini göndermek için bakımjob'u tetikle.
            yedeklemebildiri.ScheduleJob(jobyedekleme, yedeklemetetikle);//yedekleme bildiri mailini yedeklemejob için lisansjob'u tetikle.
        }
    }
}