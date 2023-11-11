using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Booking.API.Context;
using System;
using Booking.API.Controllers;
using Booking.API.Models;
using System.Threading.Tasks;
using System.Linq;
using log4net;


namespace Booking.Schedular
{
    public class ManageSchedular
    {
        public static void Start()
        {
            IScheduler scheduler = (IScheduler)StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<WaitListRefund>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(s =>
                s.WithIntervalInHours(24)
                .OnEveryDay()
                .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(12, 42))
                ).Build();

            scheduler.ScheduleJob(job, trigger);
        }
        public class WaitListRefund : IJob
        {
             private readonly BookingContext _db;
            public WaitListRefund(BookingContext db)
            {
                _db = db;
            }
            public async Task Execute(IJobExecutionContext context)
            {
                var ClassStarDates = await _context.Class.Where(x => x.StartTime < DateTime.Now).ToListAsync();
                foreach (var item in ClassStarDates)
                {
                    var endDate  = item.StartTime.AddDays(item.Duration);
                    if(endDate == DateTime.Now)
                    {
                        var waitList = await _contex.WaitList.Where(x =>x.ClassId == item.ClassId).ToList();
                        
                    }
                }
                
                
            }
        }
    }


}