using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.PL.WebApplication.ViewModels.Shared;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class ClientsViewModel : LayoutViewModel
    {
        public IEnumerable<IClient> Clients;
        public IClient Client { get; set; }
        public ClientFormViewModel ClientFormViewModel { get; set; }

        public ClientsViewModel(ClientFormViewModel clientFormViewModel, IEnumerable<IClient> clients)
        {
            HeaderViewModel.NavbarViewModel.ActiveItem = NavbarViewModel.NavItems.Clients;

            ClientFormViewModel = clientFormViewModel;
            Clients = clients;

        }

    }
}