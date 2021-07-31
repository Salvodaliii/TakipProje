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
            IJobDetail jobbakim = JobBuilder.Create<BakimEmailJob>().Build();
            IJobDetail jobyedekleme = JobBuilder.Create<YedeklemeEmailJob>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule
            (s =>s.WithIntervalInMinutes(1).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))).Build();

            //scheduler.ScheduleJob(job, trigger);
            //scheduler.ScheduleJob(jobbakim, trigger);
            //scheduler.ScheduleJob(jobyedekleme, trigger);
        }
    }
}