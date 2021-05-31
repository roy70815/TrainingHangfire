using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingHangfire.Work.Job.Interface
{
    public interface IStockScheduleMethod
    {
        Task<bool> GetStockEveryDayInfoAsync();
    }
}
