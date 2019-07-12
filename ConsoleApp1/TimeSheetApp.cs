using System;
using System.Data;
using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.Repository.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.BLL.Service.Implementation;

namespace TimeSheetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid TestCountryId = Guid.Parse("cb77cce6-c2cb-473b-bdd2-5dac8c93b756");
            Guid TestClientId = Guid.Parse("cb77cce6-c2cb-472b-bdd1-5dac8c93b756");
            List<ICountry> countryList = new List<ICountry>() { new Country(TestCountryId, "Srbija") };
            List<IClient> clientList = new List<IClient>() { new Client(TestClientId, "Max", "Ns", "Addr", "2122", TestCountryId) };
            EmptyTables();
            foreach (ICountry country in countryList)
            {
                ExecuteQuery($"INSERT INTO [dbo].[Country] ([Id], [Name]) VALUES (N'{country.Id}', N'{country.Name}');");
            }

            foreach (IClient client in clientList)
            {
                ExecuteQuery($"INSERT INTO [dbo].[Client] ([Id], [Name], [Address], [City], [ZipCode], [CountryId]) VALUES (N'{client.Id}', N'{client.Name}', N'{client.City}', N'{client.Address}', N'{client.ZipCode}', N'{client.CountryId}');");
            }

            //

            IClientService cs = new ClientService(new ClientDAL(new DBService(new DbConnectionService("Connection"))));
            var x = cs.GetClientById(TestClientId);
            Console.WriteLine(x.Name);
        }

        private static void ExecuteQuery(string sql)
        {
            using (IDbConnection connection = new DBService(new DbConnectionService("Connection")).CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand(sql);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void EmptyTables()
        {
            ExecuteQuery("DELETE FROM Client");
            ExecuteQuery("DELETE FROM Country");
        }
    }
}
