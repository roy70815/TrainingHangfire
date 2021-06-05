using Dapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using TrainingHangfire.Common.Constants;
using TrainingHangfire.Common.Helpers.DapperHelper;
using TrainingHangfire.Common.Helpers.DatabaseHelper;
using TrainingHangfireRepositoryTests.Helper.Database;

namespace TrainingHangfireRepositoryTests.Implement
{
    public class BaseRepositoryTests
    {
        public static void CreateTable(string[] tables,string databaseName)
        {
            foreach (var table in tables)
            {
                using (var connection = TestHook.GetTestDatabase().GetDBConnection(databaseName))
                {
                    var path = Path.Combine("TestData","Table",$"{table}.sql");
                    var sqlCommand = File.ReadAllText(path);
                    connection.Open();
                    connection.Execute(sqlCommand);
                }
            }
        }

        public static void InsertData(string[] tables, string databaseName)
        {
            foreach (var table in tables)
            {
                using (var connection = TestHook.GetTestDatabase().GetDBConnection(databaseName))
                {
                    var path = Path.Combine("TestData", "Data", $"{table}.sql");
                    var sqlCommand = File.ReadAllText(path);
                    connection.Open();
                    connection.Execute(sqlCommand);
                }
            }
        }

        public static void TruncateTable(string[] tables, string databaseName)
        {
            foreach (var table in tables)
            {
                using (var connection = TestHook.GetTestDatabase().GetDBConnection(databaseName))
                {
                    
                    var sqlCommand = $"TRUNCATE TABLE {table}";
                    connection.Open();
                    connection.Execute(sqlCommand);
                }
            }
        }

        public static void DropTable(string[] tables, string databaseName)
        {
            foreach (var table in tables)
            {
                using (var connection = TestHook.GetTestDatabase().GetDBConnection(databaseName))
                {

                    var sqlCommand = $"DROP TABLE {table}";
                    connection.Open();
                    connection.Execute(sqlCommand);
                }
            }
        }

        public static IDataBaseHelper GetTestDatabaseHelper()
        {
            var stockConnection = TestHook.GetTestDatabase().GetDBConnection(ProjectConstants.DatabaseName.Stock);
            var leetCodeConnection = TestHook.GetTestDatabase().GetDBConnection(ProjectConstants.DatabaseName.LeetCode);

            var databaseHelper = Substitute.For<IDataBaseHelper>();
            databaseHelper.GetLeetCodeConnection().Returns(leetCodeConnection);
            databaseHelper.GetStockConnection().Returns(stockConnection);

            return databaseHelper;
        }

        public static IDapperHelper GetDapperHelper()
        {
            return new DapperHelper();
        }
    }
}
