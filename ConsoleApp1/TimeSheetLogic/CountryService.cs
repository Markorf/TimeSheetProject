using System;
using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.DAL.Repositories.Repository.Interfaces;

namespace TimeSheet.BLL.Service.Implementation
{
    public class CountryService : ICountryService
    {
        public ICountryDAL CountryDAL { get; }

        public CountryService(ICountryDAL countryDAL)
        {
            if (countryDAL == null)
            {
                throw new ArgumentNullException("Argument cannot be null", nameof(countryDAL));
            }
            CountryDAL = countryDAL;
        }

        public IEnumerable<ICountry> GetCountries()
            => CountryDAL.GetCountries();
    }
}
