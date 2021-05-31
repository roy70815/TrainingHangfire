using Hangfire;
using Hangfire.MissionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingHangfire.Work.Job.Interface;

namespace TrainingHangfire.Work.Launcher
{
    [MissionLauncher(CategoryName = "股票相關排程")]
    public class StockLauncher
    {
        [Mission(Name = "取得股票每日收盤資訊",
        Description = "取得股票每日收盤資訊")]
        public void GetStockEveryDayInfoAsync()
        {
            var jobId = BackgroundJob.Enqueue<IStockScheduleMethod>(x => x.GetStockEveryDayInfoAsync());

        }
    }
}
