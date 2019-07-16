using System;
using System.Collections.Generic;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using System.Data;
using TimeSheet.DAL.Repositories.Database.Implementation;

namespace TimeSheet.DAL.Repositories.Repository.Implementation
{
    public class CountryDAL : ICountryDAL
    {
        private IDbService _DbService;
        private DbConnectionService dbConnectionService;

        public CountryDAL(IDbService DbService)
        {
            if (DbService == null)
            {
                throw new ArgumentNullException("Value cannot be null", nameof(DbService));
            }
            _DbService = DbService;
        }

        public CountryDAL(DbConnectionService dbConnectionService)
        {
            this.dbConnectionService = dbConnectionService;
        }

        public IEnumerable<ICountry> GetCountries()
        {
            List<ICountry> countryList = new List<ICountry>() { };
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("SELECT * FROM Country");

                    using (IDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            countryList.Add(new Country(dataReader.GetSafeGuid(0), dataReader.GetSafeString(1)));
                        }
                    }
                }
            }

            return countryList;
        }
    }
}

