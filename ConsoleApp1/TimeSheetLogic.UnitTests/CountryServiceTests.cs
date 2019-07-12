using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.BLL.Service.Implementation;
using TimeSheet.Shared.Models.Implementation;

namespace TimeSheetLogic.UnitTests
{
    [TestClass]
    public class CountryServiceTests
    {
        private ICountryDAL _countryDAL;
        private List<ICountry> _countryList;

        [TestInitialize]
        public void TestInitialize()
        {
            _countryDAL = Substitute.For<ICountryDAL>();
            _countryList = new List<ICountry>() { new Country(Guid.NewGuid(), "Japan"), new Country(Guid.NewGuid(), "France"), new Country(Guid.NewGuid(), "Spain") };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
         "Argument cannot be null")]
        public void CountryService_InitWithNullArg_ReturnsException()
        {
            new CountryService(null);
        }

        [TestMethod]
        public void CountryService_InitWithValidCountryDAL_ReturnsCountryServiceInstance()
        {
            // Arrange
            CountryService countryService = new CountryService(_countryDAL);
            // Assert
            Assert.IsTrue(countryService != null);
        }

        [TestMethod]
        public void GetCountries_GetsCountryList_ReturnsCorrectCountryList()
        {
            // Arrange
            _countryDAL.GetCountries().Returns(_countryList);
            CountryService countryService = new CountryService(_countryDAL);

            // Act
            List<ICountry> countryList = countryService.GetCountries().ToList();

            // Assert
            Assert.IsTrue(countryList.Count() == _countryList.Count());
        }

        [TestMethod]
        public void GetCountries_GetsEmptyCountryList_ReturnsCorrectCountryList()
        {
            // Arrange
            _countryDAL.GetCountries().Returns(new List<ICountry>() { });
            CountryService countryService = new CountryService(_countryDAL);
            // Act
            List<ICountry> countryList = countryService.GetCountries().ToList();
            // Assert
            Assert.IsTrue(countryList.Count() == 0);
        }
    }
}
