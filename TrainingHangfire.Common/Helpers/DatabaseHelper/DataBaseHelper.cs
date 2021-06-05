using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TrainingHangfire.Common.Constants;

namespace TrainingHangfire.Common.Helpers.DatabaseHelper
{
    public class DataBaseHelper : IDataBaseHelper
    {
        private IConfiguration _configuration;

        public DataBaseHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetHangFireConnection() => GetConnectionString(ProjectConstants.DatabaseName.HangFire);

        public IDbConnection GetLeetCodeConnection() => GetConnectionString(ProjectConstants.DatabaseName.LeetCode);

        public IDbConnection GetStockConnection() => GetConnectionString(ProjectConstants.DatabaseName.Stock);

        private SqlConnection GetConnectionString(string dbName)
        {
            var connectionString = string.Format(_configuration["ConnectionStrings:DBConnection"], dbName);
            return new SqlConnection(connectionString);
        }
    }
}
