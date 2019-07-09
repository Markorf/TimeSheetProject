using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using TimeSheet.DAL.Repositories.Database.Interfaces;

namespace TimeSheet.DAL.Repositories.Database.Implementation
{
    public class DBService : IDbService
    {
        private string _ConnectionString;
        private DbProviderFactory _DBFactory;

        public DBService(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Value cannot be null or empty string", nameof(connectionString));
            }
            _ConnectionString = connectionString;
            _DBFactory = DbProviderFactories.GetFactory(GetConnectionSettings().ProviderName);

        }

        public DbConnection CreateDbConnection()
        {
            DbConnection connection = _DBFactory.CreateConnection();
            connection.ConnectionString = GetConnectionSettings().ConnectionString;
            return connection;
        }

        public DbDataAdapter CreateDbDataAdapter()
                => _DBFactory.CreateDataAdapter();

        public ConnectionStringSettings GetConnectionSettings()
        {
            if (ConfigurationManager.ConnectionStrings[_ConnectionString] == null)
            {
                throw new ConfigurationErrorsException("Provide valid connection string");
            }
            return ConfigurationManager.ConnectionStrings[_ConnectionString];
        }
    }
}
