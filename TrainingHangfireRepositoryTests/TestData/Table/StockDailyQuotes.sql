CREATE TABLE [dbo].[StockDailyQuotes](
	[StockId] [nvarchar](50) NOT NULL,
	[StockName] [nvarchar](50) NOT NULL,
	[TradingVolume] [bigint] NOT NULL,
	[TransactionMoney] [bigint] NOT NULL,
	[OpenPrice] [float] NULL,
	[HighPrice] [float] NULL,
	[LowPrice] [float] NULL,
	[ClosePrice] [float] NULL,
	[Spread] [float] NOT NULL,
	[TotalTransactionNember] [bigint] NOT NULL,
	[CreateTime] [date] NOT NULL
) ON [PRIMARY]


ALTER TABLE [dbo].[StockDailyQuotes] ADD  CONSTRAINT [DF_StockDailyQuotes_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
