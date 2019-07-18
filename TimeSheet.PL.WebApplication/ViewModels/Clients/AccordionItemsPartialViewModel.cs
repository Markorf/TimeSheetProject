using System;
using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class AccordionItemsPartialViewModel
    {
        public ClientFormPartialViewModel ClientFormPartialViewModel { get; } = new ClientFormPartialViewModel();
        public IEnumerable<IClient> Clients { get; set; } = new List<IClient>();
    }
}