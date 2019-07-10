using System;
using System.Data.SqlClient;
using TimeSheet.DAL.Repositories.DbService.Interfaces;

namespace TimeSheet.DAL.Repositories.DbService.Implementation
{
    public class DBService : IDbService
    {
        public IDbConnectionService DbConnectionService { get; set; }

        public DBService(IDbConnectionService dbConnectionService)
        {
            if (dbConnectionService == null)
            {
                throw new ArgumentNullException("Value cannot be null", nameof(dbConnectionService));
            }

            DbConnectionService = dbConnectionService;
        }

        public SqlConnection CreateDbConnection()
        {
            SqlConnection connection = new SqlConnection(DbConnectionService.GetConnectionSettings().ConnectionString);
            return connection;
        }

    }
}
