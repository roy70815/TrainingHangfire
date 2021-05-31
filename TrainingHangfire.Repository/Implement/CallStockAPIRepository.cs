using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrainingHangfire.Repository.Interface;
using TrainingHangfire.Repository.Models;

namespace TrainingHangfire.Repository.Implement
{
    public class CallStockAPIRepository : ICallStockAPIRepository
    {
        public async Task<IEnumerable<StockModel>> GetStockEveryDayInfoAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync("http://www.twse.com.tw/exchangeReport/STOCK_DAY_ALL?response=open_data");
                response = response.Replace(@"""", "");
                var datas = CsvReader.ParseLines(response);
                var stockModelCollection = new List<StockModel>();

                for (int i = 1; i < datas.Count; i++)
                {
                    var stockInfo = datas[i].Split(",");

                    stockModelCollection.Add(new StockModel
                    {
                        StockId = stockInfo[0],
                        StockName = stockInfo[1],
                        TradingVolume = string.IsNullOrEmpty(stockInfo[2]) ? 0 : Convert.ToInt64(stockInfo[2]),
                        TransactionMoney = string.IsNullOrEmpty(stockInfo[3]) ? 0 : Convert.ToInt64(stockInfo[3]),
                        OpenPrice = string.IsNullOrEmpty(stockInfo[4]) ? (double?)null : Convert.ToDouble(stockInfo[4]),
                        HighPrice = string.IsNullOrEmpty(stockInfo[5]) ? (double?)null : Convert.ToDouble(stockInfo[5]),
                        LowPrice = string.IsNullOrEmpty(stockInfo[6]) ? (double?)null : Convert.ToDouble(stockInfo[6]),
                        ClosePrice = string.IsNullOrEmpty(stockInfo[7]) ? (double?)null : Convert.ToDouble(stockInfo[7]),
                        Spread = string.IsNullOrEmpty(stockInfo[8]) ? 0 : Convert.ToDouble(stockInfo[8]),
                        TotalTransactionNember = string.IsNullOrEmpty(stockInfo[9]) ? 0 : Convert.ToInt64(stockInfo[9])

                    });

                }

                return stockModelCollection;
            }
        }
    }
}
