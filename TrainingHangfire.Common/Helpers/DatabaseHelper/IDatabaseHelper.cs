using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TrainingHangfire.Common.Helpers.DatabaseHelper
{
    public interface IDataBaseHelper
    {
        IDbConnection GetLeetCodeConnection();

        IDbConnection GetHangFireConnection();

        IDbConnection GetStockConnection();
    }
}
