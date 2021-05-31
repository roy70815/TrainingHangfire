using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace TrainingHangfire.Common.Helpers.DapperHelper
{
    /// <summary>
    /// Dapper介面
    /// </summary>
    public interface IDapperHelper
    {
        T QueryFirstOrDefault<T>(IDbConnection dbConnection, string sql, object param = null);

        Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection dbConnection, string sql, object param = null);

        IEnumerable<T> Query<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        int Execute(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<int> ExecuteAsync(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
