using System;
using System.Data;
using System.Data.Common;
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

        public IDbConnection CreateDbConnection()
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(DbConnectionService.GetConnectionSettings().ProviderName);
            DbConnection connection = dbFactory.CreateConnection();
            connection.ConnectionString = DbConnectionService.GetConnectionSettings().ConnectionString;
            return connection;
        }
    }
}
