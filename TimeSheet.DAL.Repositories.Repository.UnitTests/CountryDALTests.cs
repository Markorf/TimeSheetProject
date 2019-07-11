using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.DAL.Repositories.Repository.Implementation;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    [TestClass]
    public class CountryDALTests
    {
        private string _connectionStringName = "Connection";
        private DbSeeder _dbSeeder;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbSeeder = new DbSeeder(_connectionStringName);
            _dbSeeder.ResetTables();
        }

        [TestMethod]
        public void CountryDAL_InitWithValidDBService_ReturnsInstance()
        {
            ICountryDAL countryDAL = new CountryDAL(new DBService(new DbConnectionService(_connectionStringName)));
            Assert.IsTrue(countryDAL != null);
        }

        [TestMethod]
        public void CountryDAL_InitWithNullDBService_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CountryDAL(null));
        }

        [TestMethod]
        public void GetCountries_GetCountriesFromNonEmptyTable_ReturnsTableWithList()
        {
            ICountryDAL countryDAL = new CountryDAL(new DBService(new DbConnectionService(_connectionStringName)));
            List<ICountry> countryList = countryDAL.GetCountries().ToList();
            Assert.IsTrue(countryList.isEqualTo(DbSeeder.countryList));
        }

        [TestMethod]
        public void GetCountries_GetCountriesFromEmptyTable_ReturnsEmptyTable()
        {
            _dbSeeder.EmptyTables();
            ICountryDAL countryDAL = new CountryDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IEnumerable<ICountry> countryList = countryDAL.GetCountries();
            Assert.IsTrue(countryList.Count() == 0);
        }
    }
}
