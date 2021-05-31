using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingHangfire.Service.Interface;
using TrainingHangfire.Work.Job.Interface;

namespace TrainingHangfire.Work.Job.Implement
{
    public class StockScheduleMethod : IStockScheduleMethod
    {
        private readonly IStockService _stockService;

        public StockScheduleMethod(IStockService stockService)
        {
            _stockService = stockService;
        }

        [AutomaticRetry(Attempts = 0)]
        public Task<bool> GetStockEveryDayInfoAsync()
        {
            return _stockService.GetStockEveryDayInfoAsync();
        }
    }
}
