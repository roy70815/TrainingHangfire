using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TrainingHangfire.Common.Helpers.DapperHelper
{
    public class DapperHelper : IDapperHelper
    {
        public int Execute(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var connection = (SqlConnection)dbConnection;
            return connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<int> ExecuteAsync(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var connection = (SqlConnection)dbConnection;
            return connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public IEnumerable<T> Query<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var connection = (SqlConnection)dbConnection;
            return connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var connection = (SqlConnection)dbConnection;
            return connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public T QueryFirstOrDefault<T>(IDbConnection dbConnection, string sql, object param = null)
        {
            var connection = (SqlConnection)dbConnection;
            return connection.QueryFirstOrDefault<T>(sql, param);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection dbConnection, string sql, object param = null)
        {
            var connection = (SqlConnection)dbConnection;
            return connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
    }
}
