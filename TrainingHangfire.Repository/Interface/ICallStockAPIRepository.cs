using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingHangfire.Repository.Models;

namespace TrainingHangfire.Repository.Interface
{
    public interface ICallStockAPIRepository
    {
        Task<IEnumerable<StockModel>> GetStockEveryDayInfoAsync();
    }
}
