using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.DAL.Repositories.List.Interfaces;
using TimeSheet.BLL.Service.Implementation;
using TimeSheet.Shared.Models.Implementation;

namespace TimeSheetLogic.UnitTests
{
    [TestClass]
    public class CountryServiceTests
    {
        ICountryDAL _countryDAL;
        List<ICountry> _countryList;

        [TestInitialize]
        public void TestInitialize()
        {
            _countryDAL = Substitute.For<ICountryDAL>();
            _countryList = new List<ICountry>() { new Country("Japan"), new Country("France"), new Country("Spain") };
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
