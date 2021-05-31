using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrainingHangfire.Service.Interface
{
    public interface IStockService
    {
        Task<bool> GetStockEveryDayInfoAsync();
    }
}
