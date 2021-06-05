using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace TrainingHangfireRepositoryTests.Helper.Database
{
    public interface ITestDatabase
    {
        Task CreateDatabase();
        Task DropDatabase();
        IDbConnection GetMasterDBConnection();
        IDbConnection GetDBConnection(string databaseName);
    }
}
