using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingHangfire.Common.Helpers.DapperHelper;
using TrainingHangfire.Common.Helpers.DatabaseHelper;
using TrainingHangfire.Repository.Interface;
using TrainingHangfire.Repository.Models;

namespace TrainingHangfire.Repository.Implement
{
    public class StockDailyQuotesRepository : IStockDailyQuotesRepository
    {
        private readonly IDapperHelper _dapperHelper;

        private readonly IDataBaseHelper _dataBaseHelper;

        public StockDailyQuotesRepository(
            IDapperHelper dapperHelper,
            IDataBaseHelper dataBaseHelper)
        {
            _dapperHelper = dapperHelper;
            _dataBaseHelper = dataBaseHelper;
        }

        public async Task<bool> CreateAsync(IEnumerable<StockModel> stockModels)
        {
            var sqlCommand = @" INSERT INTO [dbo].[StockDailyQuotes]
                                      ([StockId]
                                      ,[StockName]
                                      ,[TradingVolume]
                                      ,[TransactionMoney]
                                      ,[OpenPrice]
                                      ,[HighPrice]
                                      ,[LowPrice]
                                      ,[ClosePrice]
                                      ,[Spread]
                                      ,[TotalTransactionNember])
                                VALUES(
                                      @StockId,
                                      @StockName,
                                      @TradingVolume,
                                      @TransactionMoney,
                                      @OpenPrice,
                                      @HighPrice,
                                      @LowPrice,
                                      @ClosePrice,
                                      @Spread,
                                      @TotalTransactionNember
                                      )";


            using (var connection = _dataBaseHelper.GetStockConnection())
            {
                var result = await _dapperHelper.ExecuteAsync(
                    connection,
                    sqlCommand,
                    stockModels
                    );

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
