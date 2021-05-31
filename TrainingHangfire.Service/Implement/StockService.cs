using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingHangfire.Repository.Interface;
using TrainingHangfire.Service.Interface;

namespace TrainingHangfire.Service.Implement
{
    public class StockService : IStockService
    {
        private readonly ICallStockAPIRepository _stockRepository;

        private readonly IStockDailyQuotesRepository _stockDailyQuotesRepository;
        public StockService(ICallStockAPIRepository stockRepository,
            IStockDailyQuotesRepository stockDailyQuotesRepository)
        {
            _stockRepository = stockRepository;
            _stockDailyQuotesRepository = stockDailyQuotesRepository;
        }

        public async Task<bool> GetStockEveryDayInfoAsync()
        {
            var stockModels = await _stockRepository.GetStockEveryDayInfoAsync();
            return await _stockDailyQuotesRepository.CreateAsync(stockModels);
        }
    }
}
