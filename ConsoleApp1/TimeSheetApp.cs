using System;
using System.Collections.Generic;
using TimeSheet.DAL.Repositories.List.Implementation;
using TimeSheet.BLL.Service.Implementation;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using System.Linq;

namespace TimeSheetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICountryService countryService = new CountryService(new CountryDAL(new DBService(null)));
            IClientService clientService = new ClientService(new ClientDAL(null));
            IEnumerable<ICountry> countryList = countryService.GetCountries();
            IEnumerable<IClient> clientList = clientService.GetClients();
            Guid id = clientList.First().Id;
            clientService.UpdateClientById(new Client("Boza") { Id = id });
        }
    }
}
