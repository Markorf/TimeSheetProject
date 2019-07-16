using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class ClientFormViewModel
    {
        public IEnumerable<ICountry> Countries { get; set; }
        public IClient Client { get; set; } = new Client();

        public ClientFormViewModel(IEnumerable<ICountry> countries)
        {
            Countries = countries;
        }

        public ClientFormViewModel() { }

        public void SetClient(IClient client)
        {
            Client = client;
        }
    }
}