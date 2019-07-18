using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.PL.WebApplication.ViewModels.Shared;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class ClientsViewModel : LayoutViewModel
    {
        public AccordionItemsPartialViewModel AccordionItemsPartialViewModel { get; } = new AccordionItemsPartialViewModel();
        public CreateClientDialogPartialViewModel CreateClientDialogPartialViewModel { get; } = new CreateClientDialogPartialViewModel();
        public FilterLettersPartialViewModel FilterLettersPartialViewModel { get; } = new FilterLettersPartialViewModel();

        public ClientsViewModel()
        {
            HeaderViewModel.NavbarViewModel.ActiveItem = NavbarViewModel.NavItems.Clients;
        }
    }
}