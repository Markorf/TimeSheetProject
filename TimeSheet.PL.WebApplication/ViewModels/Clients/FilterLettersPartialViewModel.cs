using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class FilterLettersPartialViewModel
    {
        public char CurrentLetter { get; set; }
        public IEnumerable<IClient> ClientList { get; set; }

        public IEnumerable<string> GetClientsInitialLetters()
            => ClientList.Select(client => client.Name.FirstOrDefault().ToString().ToUpper());

    }
}