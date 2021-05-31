using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingHangfire.Repository.Models
{
    /// <summary>
    /// 股票資訊
    /// </summary>
    public class StockModel
    {
        /// <summary>
        /// 證券代號
        /// </summary>
        [JsonProperty("stockId")]
        public string StockId { get; set; }
        /// <summary>
        /// 證券名稱
        /// </summary>
        [JsonProperty("stockName")]
        public string StockName { get; set; }

        /// <summary>
        /// 成交股數
        /// </summary>
        [JsonProperty("tradingVolume")]
        public Int64 TradingVolume { get; set; }

        /// <summary>
        /// 成交金額
        /// </summary>
        [JsonProperty("transactionMoney")]
        public Int64 TransactionMoney { get; set; }

        /// <summary>
        /// 開盤價
        /// </summary>
        [JsonProperty("openPrice")]
        public double? OpenPrice { get; set; }

        /// <summary>
        /// 最高價
        /// </summary>
        [JsonProperty("highPrice")]
        public double? HighPrice { get; set; }

        /// <summary>
        /// 最低價
        /// </summary>
        [JsonProperty("lowPrice")]
        public double? LowPrice { get; set; }

        /// <summary>
        /// 收盤價
        /// </summary>
        [JsonProperty("closePrice")]
        public double? ClosePrice { get; set; }

        /// <summary>
        /// 漲跌價差
        /// </summary>
        [JsonProperty("spread")]
        public double Spread { get; set; }

        /// <summary>
        /// 成交筆數
        /// </summary>
        [JsonProperty("totalTransactionNember")]
        public Int64 TotalTransactionNember { get; set; }
    }
}
