using System;
using System.Collections.Generic;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using System.Data.SqlClient;

namespace TimeSheet.DAL.Repositories.Repository.Implementation
{
    public class CountryDAL : ICountryDAL
    {
        private IDbService _DbService;

        public CountryDAL(IDbService DbService)
        {
            if (DbService == null)
            {
                throw new ArgumentNullException("Value cannot be null", nameof(DbService));
            }
            _DbService = DbService;
        }

        public IEnumerable<ICountry> GetCountries()
        {
            List<ICountry> countryList = new List<ICountry>();
            using (SqlConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Country;", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    countryList.Add(new Country(reader.GetString(1)));
                }
            }
            return countryList;
        }
    }
}
