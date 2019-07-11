using System;
using System.Data;
using System.Collections.Generic;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.Repository.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    public class DbSeeder
    {
        public static Guid TestClientId = Guid.Parse("cb77cce6-c2cb-472b-bdd1-5dac8c93b756");
        public static Guid TestCountryId = Guid.Parse("cb77cce6-c2cb-473b-bdd2-5dac8c93b756");
        public static string TestClientName = "Max";
        public static List<ICountry> countryList = new List<ICountry>() { new Country(TestClientId, "Srbija") };
        public static List<IClient> clientList = new List<IClient>() { new Client(TestClientId, TestClientName, "Ns", "Addr", "2122", TestClientId) };
        private IDbService _dbService;

        public DbSeeder(string connectionStringName)
        {
            _dbService = new DBService(new DbConnectionService(connectionStringName));
        }

        public void ResetTables()
        {
            ExecuteQuery("DELETE FROM Client");
            ExecuteQuery("DELETE FROM Country");

            foreach (ICountry country in countryList)
            {
                ExecuteQuery($"INSERT INTO [dbo].[Country] ([Id], [Name]) VALUES (N'{country.Id}', N'{country.Name}');");
            }

            foreach (IClient client in clientList)
            {
                ExecuteQuery($"INSERT INTO [dbo].[Client] ([Id], [Name], [Address], [City], [ZipCode], [CountryId]) VALUES (N'{client.Id}', N'{client.Name}', N'{client.City}', N'{client.Address}', N'{client.ZipCode}', N'{client.CountryId}');");
            }
        }

        public void EmptyTables()
        {
            ExecuteQuery("DELETE FROM Client");
            ExecuteQuery("DELETE FROM Country");
        }

        private void ExecuteQuery(string sql)
        {
            using (IDbConnection connection = _dbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand(sql);
                    command.ExecuteNonQuery();
                }
            }
        }

    }

}

