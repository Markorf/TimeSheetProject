using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

namespace TimeSheet.DAL.Repositories.Database.Interfaces
{
    public interface IDbService
    {
        DbConnection CreateDbConnection();
        DbDataAdapter CreateDbDataAdapter();
        ConnectionStringSettings GetConnectionSettings();
    }
}
