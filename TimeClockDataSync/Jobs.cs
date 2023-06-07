using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeClockDataSync.DataTransfer;

namespace TimeClockDataSync
{
    public static class Jobs
    {
        public static string GetJobDate()
        {
            int MinuteFromConf = 1;

            int Second = DateTime.Now.Second;
            int Minute = DateTime.Now.AddMinutes(MinuteFromConf).Minute;
            int Hour = DateTime.Now.Hour;
            int Day = DateTime.Now.Day;
            string Month = DateTime.Now.ToString("MMM");
            int Year = DateTime.Now.Year;

            if (MinuteFromConf != 0)
            {

                return string.Format("{0} {1} {2} {3} {4} ? {5}", Second, Minute, Hour, Day, Month, Year);
            }
            else
            {
                return string.Format("{0} {1} {2} {3} {4} ? {5}", Second, Minute, Hour, Day, Month, 2090);
            }


        }

        public static async void InitializeJobs(string ConnectionString , string ApiDomain)
        {

            try
            {
                Console.WriteLine("Inttializing Jobs");

                var _scheduler = await new StdSchedulerFactory().GetScheduler();
                await _scheduler.Start();


                var SalesDataJob = JobBuilder.Create<SalesDataJob>().UsingJobData("ConnectionString", ConnectionString)
             .WithIdentity("SalesDataJob")
             .Build();

                var SalesDataTrigger = TriggerBuilder.Create()
                  .WithIdentity("SalesDataCron")
                  .StartNow()
                  .WithCronSchedule(ConfigurationManager.AppSettings["SalesDataInterval"].ToString())
                  .Build();

                var result2 = await _scheduler.ScheduleJob(SalesDataJob, SalesDataTrigger);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }



        public static async Task SalesData(string ConnectionString)
        {
            try
            {
                FromDb.SyncSales(ConnectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
    }

    public class SalesDataJob : IJob
    {
        public async Task Execute(IJobExecutionContext Context)
        {
            var Data = Context.JobDetail.Clone();
            string ConnectionbString = Data.JobDataMap.Get("ConnectionString").ToString();
            await Jobs.SalesData(ConnectionbString);
            return;
        }
    }

}
