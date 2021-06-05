using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingHangfire.Common.Constants;
using TrainingHangfire.Common.Helpers.DapperHelper;
using TrainingHangfire.Common.Helpers.DatabaseHelper;
using TrainingHangfire.Repository.Implement;
using TrainingHangfire.Repository.Interface;
using TrainingHangfire.Repository.Models;
using TrainingHangfireRepositoryTests.Helper.Database;

namespace TrainingHangfireRepositoryTests.Implement
{
    [TestClass]
    public class StockDailyQuotesRepositoryTests : BaseRepositoryTests
    {

        private IDapperHelper _dapperHelper;

        private IDataBaseHelper _databaseHelper;

        private static string[] tables =
        {
            "StockDailyQuotes"
        };

        private IStockDailyQuotesRepository GetSystemUnderTest()
        {
            return new StockDailyQuotesRepository(_dapperHelper, _databaseHelper);
        }

        [ClassInitialize]
        public static void StockDailyQuotesRepositoryTestsInitialize(TestContext testContext)
        {
            CreateTable(tables, ProjectConstants.DatabaseName.Stock);
        }

        [ClassCleanup]
        public static void StockDailyQuotesRepositoryTestsCleanup()
        {
            DropTable(tables, ProjectConstants.DatabaseName.Stock);
        }
        [TestInitialize]
        public void TestInitialize()
        {
            InsertData(tables, ProjectConstants.DatabaseName.Stock);
            _databaseHelper = GetTestDatabaseHelper();
            _dapperHelper = GetDapperHelper();
        }
        [TestCleanup]
        public void TestCleanup()
        {
            TruncateTable(tables, ProjectConstants.DatabaseName.Stock);
        }

        [TestMethod]
        [Owner("Roy")]
        [TestCategory("StockDailyQuotesRepository")]
        [TestProperty("StockDailyQuotesRepository", "CreateAsync")]
        public async Task CreateAsync_傳入參數_應回傳True()
        {
            //arrange
            var fixture = new Fixture();
            var stockModels = fixture.CreateMany<StockModel>(3);
            var target = GetSystemUnderTest();

            //act
            var actual = await target.CreateAsync(stockModels);

            //assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        [Owner("Roy")]
        [TestCategory("StockDailyQuotesRepository")]
        [TestProperty("StockDailyQuotesRepository", "CreateAsync")]
        public async Task CreateAsync_傳入參數Collection數量為0_應回傳False()
        {
            //arrange
            var fixture = new Fixture();
            var stockModels = fixture.CreateMany<StockModel>(0);
            var target = GetSystemUnderTest();

            //act
            var actual = await target.CreateAsync(stockModels);

            //assert
            actual.Should().BeFalse();
        }
    }
}
