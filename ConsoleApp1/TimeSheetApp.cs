using System;
using System.Linq;
using System.Collections.Generic;
using TimeSheet.BLL.Service.Implementation;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.Repository.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using TimeSheet.Shared.Models.Implementation;

namespace TimeSheetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICountryService countryService = new CountryService(new CountryDAL(new DBService(new DbConnectionService("Connection"))));
            IClientService clientService = new ClientService(new ClientDAL(new DBService(new DbConnectionService("Connection"))));
            IEnumerable<ICountry> countryList = countryService.GetCountries();
            IEnumerable<IClient> clientList = clientService.GetClients();
            Guid id = clientList.First().Id;
            clientService.AddClient(new Client(Guid.NewGuid(), "ZXC"));
            Console.WriteLine(clientList.First().Name);
            clientService.UpdateClientById(new Client(Guid.NewGuid(), "ZXC"));
            var x = new List<string>() { "Max" };
            var y = new List<string>() { "Max", "Mix" };
            var rez = x.Except(y);
            Console.WriteLine(rez.Count());

        }
    }
}
