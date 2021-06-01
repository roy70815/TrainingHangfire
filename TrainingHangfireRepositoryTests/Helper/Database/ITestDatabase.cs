using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TrainingHangfireRepositoryTests.Helper.Database
{
    public interface ITestDatabase
    {
        void CreateDatabase();
        void DropDatabase();
        IDbConnection GetMasterDBConnection();
    }
}
