using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TrainingHangfireRepositoryTests.Helper.Database
{
    public class TestDatabase : ITestDatabase
    {
        private string[] _databaseNames =
        {
            "Stock"
        };
        public void CreateDatabase()
        {
            DockerHandler.CreateContainer();

            foreach (var databaseName in _databaseNames)
            {
                if (IsDatabaseExist(databaseName))
                {
                    continue;
                }

                using (var connection = GetMasterDBConnection())
                {
                    var sqlCommand = $"CREATE DATABASE @DatabaseName COLLATE Chinese_Taiwan_Stroke_CI_AI";

                    var parameter = new DynamicParameters();
                    parameter.Add("DatabaseName", databaseName);
                    connection.Execute(sqlCommand);
                }
            }
        }

        public void DropDatabase()
        {
            DockerHandler.RemoveExistContainer();
        }

        public IDbConnection GetMasterDBConnection()
        {
            var connectionString = @$"Server=localhost,{DockerHandler._linux_mssql_port} Initial Catalog=master; 
                                    User ID=sa; Password=!@#QWEasd; Trusted_Connection=True; Integrated Security = false;";

            return new SqlConnection(connectionString);
        }

        private bool IsDatabaseExist(string databaseName)
        {
            using (var connection = GetMasterDBConnection())
            {
                var sqlcommand = @$"if EXISTS(SELECT * FROM sysdatabases WHERE name=@DatabaseName)
                                        select 1
                                    else
                                        select 0";
                var parameter = new DynamicParameters();
                parameter.Add("DatabaseName", databaseName);

                var result = connection.QueryFirstOrDefault<bool>(sqlcommand, parameter);
                return result;
            }
        }
    }
}
