using System;
using System.Configuration;
using TimeSheet.DAL.Repositories.DbService.Interfaces;

namespace TimeSheet.DAL.Repositories.Database.Implementation
{
    public class DbConnectionService : IDbConnectionService
    {
        private string _connectionName;

        public DbConnectionService(string connectionName)
        {
            if (String.IsNullOrEmpty(connectionName))
            {
                throw new ArgumentException("Value cannot be null or empty string", nameof(connectionName));
            }
            _connectionName = connectionName;
        }

        public ConnectionStringSettings GetConnectionSettings()
            => ConfigurationManager.ConnectionStrings[_connectionName] ?? throw new ConfigurationErrorsException("Connection string not found");
    }
}
