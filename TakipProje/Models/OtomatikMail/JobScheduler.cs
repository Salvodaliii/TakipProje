using Quartz;
using Quartz.Impl;
using System;

namespace TakipProje.Models.OtomatikMail
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInSeconds(15) //15 saniyede bir
                   //.OnEveryDay()   //her gün
                   //.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}