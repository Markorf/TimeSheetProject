using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using System.Data;
using TimeSheet.DAL.Repositories.Database.Implementation;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    [TestClass]
    public class DbServiceTests
    {
        private string _connectionName = "Connection";
        [TestMethod]
        public void DbService_InitWithValidConnectionService_ReturnsDBServiceInstance()
        {
            IDbService dbService = new DBService(new DbConnectionService(_connectionName));
            Assert.IsTrue(dbService != null);
        }

        [TestMethod]
        public void DbService_InitWithEmptyConnectionString_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DBService(null));
        }

        [TestMethod]
        public void CreateDBConnection_CallWithValidConnectionString_ReturnsConnection()
        {
            IDbService dbService = new DBService(new DbConnectionService(_connectionName));
            IDbConnection connection = dbService.CreateDbConnection();
            Assert.IsTrue(connection != null);
        }

        [TestMethod]
        public void CreateDBConnection_CallWithNullInsteadConnectionService_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DBService(null).CreateDbConnection());
        }

        [TestMethod]
        public void CreateDBConnection_CallWithInValidConnectionName_ThrowsException()
        {
            Assert.ThrowsException<ConfigurationErrorsException>(() => new DBService(new DbConnectionService(_connectionName + "-Invalid")).CreateDbConnection());
        }
    }
}
