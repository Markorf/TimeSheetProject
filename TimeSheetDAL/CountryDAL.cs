using System;
using System.Collections.Generic;
using System.Data.Common;
using TimeSheet.DAL.Repositories.List.Interfaces;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.Database.Interfaces;
using System.Data;

namespace TimeSheet.DAL.Repositories.List.Implementation
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
            DataSet ds = new DataSet("Country");
            using (DbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (DbDataAdapter dbDataAdapter = _DbService.CreateDbDataAdapter())
                {
                    (dbDataAdapter as IDbDataAdapter).SelectCommand = connection.CreateCommand("SELECT * FROM Country;");
                    dbDataAdapter.Fill(ds);
                }
            }
            return ds.Tables[0].AsEnumerable().Select(row => MapRowToCountry(row));
        }

        private ICountry MapRowToCountry(DataRow row)
            => new Country(row.Field<string>("Name"));
    }
}
