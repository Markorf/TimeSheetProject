using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.Shared.Models.Implementation;

namespace TimeSheet.Shared.Models.UnitTests
{
    [TestClass]
    public class CountryModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Name can't be null or empty string")]
        public void Country_InitWithNullCountryName_ReturnsArgumentException()
        {
            Country country = new Country(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Name can't be null or empty string")]
        public void Country_InitWithEmptyCountryName_ReturnsArgumentException()
        {
            Country country = new Country("");
        }

        [TestMethod]
        public void Country_InitWithValidCountryName_ReturnsCountryInstance()
        {
            Country country = new Country("Malta");
            Assert.IsTrue(country != null);
        }
    }
}
