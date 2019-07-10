using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    [TestClass]
    public class CountryDALTests
    {
        private string _connectionString = "TimeSheet.DAL.Repositories.Repository.UnitTests.Properties.Settings.TimeSheetConnection";
        private DbSeeder _dbSeeder;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbSeeder = new DbSeeder(_connectionString);
            _dbSeeder.PopulateCountryTable();
        }
    }
}
