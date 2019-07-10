using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using TimeSheet.DAL.Repositories.Database.Implementation;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    [TestClass]
    public class DbConnectionServiceTests
    {
        [TestMethod]
        public void DbConnectionService_InitWithValidConnectionString_ReturnsDbConnectionInstanceInstance()
        {
            IDbConnectionService dbConnectionService = new DbConnectionService("Connection");
            Assert.IsTrue(dbConnectionService != null);
        }

        [TestMethod]
        public void DbConnectionService_InitWithEmptyString_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new DbConnectionService(""));
        }

        [TestMethod]
        public void DbConnectionService_InitWithNull_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new DbConnectionService(null));
        }

        [TestMethod]
        public void GetConnectionSettings_CallWithValidConnectionString_ReturnsConnectionStringSettings()
        {
            IDbConnectionService dbConnectionService = new DbConnectionService("Connection");
            ConnectionStringSettings result = dbConnectionService.GetConnectionSettings();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetConnectionSettings_CallWithInvalidConnectionString_ThrowsException()
        {
            IDbConnectionService dbConnectionService = new DbConnectionService("ConnectionXx");
            Assert.ThrowsException<ConfigurationErrorsException>(() => dbConnectionService.GetConnectionSettings());

        }
    }
}
