using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TimeSheet.PL.WebApplication.ViewModels.Clients;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.PL.WebApplication.ViewModels.Shared;

namespace TimeSheet.PL.WebApp.Controllers
{
    [RoutePrefix("clients")]
    public class ClientsController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IClientService _clientService;
        private ClientsViewModel _clientsViewModel = new ClientsViewModel();

        public ClientsController(ICountryService countryService, IClientService clientService)
        {
            _countryService = countryService;
            _clientService = clientService;
            int rowsPerPage = _clientsViewModel.PaginationPartialViewModel.RowsPerPage;

            IEnumerable<ICountry> countryList = _countryService.GetCountries();
            IEnumerable<IClient> clientList = _clientService.GetClients();
            IEnumerable<IClient> fetchedClients = _clientService.GetClientsByPaging(_clientsViewModel.PaginationPartialViewModel.RowsOffset, rowsPerPage);
            _clientsViewModel.CreateClientDialogPartialViewModel.Countries = countryList;
            _clientsViewModel.AccordionItemsPartialViewModel.ClientFormPartialViewModel.Countries = countryList;
            _clientsViewModel.AccordionItemsPartialViewModel.Clients = fetchedClients;
            _clientsViewModel.FilterLettersPartialViewModel.ClientList = clientList;
            decimal diff = (decimal)clientList.Count() / rowsPerPage;
            _clientsViewModel.PaginationPartialViewModel.PageCount = (int)Math.Ceiling(diff);
        }

        [Route("")]
        public ActionResult Index()
        {
            return View("Index", _clientsViewModel);
        }

        [Route("update")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ClientFormPartialViewModel form)
        {
            if (!ModelState.IsValid)
            {
                _clientsViewModel.AccordionItemsPartialViewModel.InvalidClientId = form.Client.Id;
                return Index();
            }
            IClient client = new ClientViewModel(form.Client.Id, form.Client.Name, form.Client.Address, form.Client.City, form.Client.ZipCode, form.Client.CountryId);
            _clientService.UpdateClientById(client);

            return RedirectToAction("Index");
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            // iskoristiti posle
            _clientService.RemoveClientById(id);
            return RedirectToAction("Index");
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateClientDialogPartialViewModel form)
        {
            if (!ModelState.IsValid)
            {
                _clientsViewModel.CreateClientDialogPartialViewModel.HasValidationError = true;
                return Index();
            }
            IClient client = new ClientViewModel(Guid.NewGuid(), form.Client.Name, form.Client.Address, form.Client.City, form.Client.ZipCode, form.Client.CountryId);
            _clientService.AddClient(client);
            return RedirectToAction("Index");
        }

        [Route("filteredByLetter")]
        [HttpGet]
        public ActionResult FilterByLetter(char letter)
        {
            IEnumerable<IClient> filteredList = _clientService.FilterClientsByFirstLetter(letter);
            _clientsViewModel.FilterLettersPartialViewModel.CurrentLetter = letter;
            _clientsViewModel.FilterLettersPartialViewModel.ClientList = _clientService.GetClients();
            SetClientsToViewModel(filteredList, false);
            return Index();
        }

        [Route("filterByName")]
        [HttpGet]
        public ActionResult FilterByName(string searchText)
        {
            IEnumerable<IClient> filteredList = _clientService.FilterClientsByName(searchText);
            SetClientsToViewModel(filteredList, false);
            return Index();
        }

        [Route("ShowClientForms")]
        [ChildActionOnly]
        public ActionResult ShowClientForms(Guid id)
        {
            IClient client = _clientService.GetClientById(id);
            _clientsViewModel.AccordionItemsPartialViewModel.ClientFormPartialViewModel.Client = client;
            return PartialView("ClientFormPartial", _clientsViewModel.AccordionItemsPartialViewModel.ClientFormPartialViewModel);
        }

        [Route("nextClients")]
        [HttpGet]
        public ActionResult LoadNextClients(int page)
        {
            int rowsPerPage = _clientsViewModel.PaginationPartialViewModel.RowsPerPage;
            _clientsViewModel.PaginationPartialViewModel.RowsOffset = page * rowsPerPage - rowsPerPage;
            IEnumerable<IClient> fetchedClients = _clientService.GetClientsByPaging(_clientsViewModel.PaginationPartialViewModel.RowsOffset, rowsPerPage);
            _clientsViewModel.AccordionItemsPartialViewModel.Clients = fetchedClients;
            _clientsViewModel.PaginationPartialViewModel.CurrentPage = page;
            return Index();
        }

        private void SetClientsToViewModel(IEnumerable<IClient> clientList = null, bool changeLettersTab = true)
        {
            if (clientList == null)
            {
                clientList = _clientService.GetClients();
            }
            if (changeLettersTab)
            {
                _clientsViewModel.FilterLettersPartialViewModel.ClientList = clientList;
            }
            _clientsViewModel.AccordionItemsPartialViewModel.Clients = clientList;
        }
    }
}