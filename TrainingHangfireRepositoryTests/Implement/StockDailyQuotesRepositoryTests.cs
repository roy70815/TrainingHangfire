using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingHangfire.Common.Helpers.DapperHelper;
using TrainingHangfire.Common.Helpers.DatabaseHelper;
using TrainingHangfire.Repository.Implement;
using TrainingHangfire.Repository.Interface;

namespace TrainingHangfireRepositoryTests.Implement
{
    [TestClass]
    public class StockDailyQuotesRepositoryTests
    {

        private IDapperHelper _dapperHelper;

        private IDataBaseHelper _databaseHelper;

        private static string[] tables =
        {
            "StockDailyQuotes"
        };

        [ClassInitialize]
        public static void StockDailyQuotesRepositoryTestsInitialize(TestContext testContext)
        {
            
        }

        [ClassCleanup]
        public static void StockDailyQuotesRepositoryTestsCleanup()
        {

        }
        [TestInitialize]
        public void TestInitialize()
        {

        }
        [TestCleanup]
        public void TestCleanup()
        {

        }

        [TestMethod]
        [Owner("Roy")]
        [TestCategory("StockDailyQuotesRepository")]
        [TestProperty("StockDailyQuotesRepository", "CreateAsync")]
        public void CreateAsync_建立個股單日價格資訊()
        {
            
        }
    }
}
