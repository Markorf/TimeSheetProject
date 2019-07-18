using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.PL.WebApplication.ViewModels.Clients;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using TimeSheet.DAL.Repositories.Repository.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.BLL.Service.Implementation;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.PL.WebApplication.ViewModels.Shared;

namespace TimeSheet.PL.WebApp.Controllers
{
    [RoutePrefix("clients")]
    public class ClientsController : Controller
    {
        private IDbService _dbService;
        private ICountryService _countryService;
        private IClientService _clientService;
        private string _connectionStringName = "Connection";
        private ClientsViewModel _clientsViewModel = new ClientsViewModel();

        public ClientsController()
        {
            _dbService = new DBService(new DbConnectionService(_connectionStringName));
            _countryService = new CountryService(new CountryDAL(_dbService));
            _clientService = new ClientService(new ClientDAL(_dbService));

            IEnumerable<ICountry> countryList = _countryService.GetCountries();
            _clientsViewModel.CreateClientDialogPartialViewModel.Countries = countryList;
            _clientsViewModel.AccordionItemsPartialViewModel.ClientFormPartialViewModel.Countries = countryList;
            _clientsViewModel.FilterLettersPartialViewModel.ClientList = _clientService.GetClients();
        }

        [Route("")]
        public ActionResult Index(IEnumerable<IClient> clientList = null)
        {
            if (clientList == null)
            {
                clientList = _clientService.GetClients();
            }
            _clientsViewModel.AccordionItemsPartialViewModel.Clients = clientList;
            return View("Index", _clientsViewModel);
        }

        [Route("update")]
        [HttpPost]
        public ActionResult Update(ClientViewModel client)
        {
            _clientService.UpdateClientById(client);
            return RedirectToAction("Index");
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            // iskoristiti posle
            bool isClientDeleted = _clientService.RemoveClientById(id);
            return RedirectToAction("Index");
        }

        [Route("create")]
        [HttpPost]
        public ActionResult Create(CreateClientDialogPartialViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }
            IClient client = new Client(Guid.NewGuid(), form.Client.Name, form.Client.Address, form.Client.City, form.Client.ZipCode, form.Client.CountryId);
            _clientService.AddClient(client);
            return RedirectToAction("Index");
        }

        [Route("FilteredByLetter")]
        [HttpGet]
        public ActionResult FilterByLetter(char letter)
        {
            IEnumerable<IClient> filteredList = _clientService.FilterClientsByFirstLetter(letter);
            _clientsViewModel.FilterLettersPartialViewModel.CurrentLetter = letter;
            _clientsViewModel.FilterLettersPartialViewModel.ClientList = _clientService.GetClients();

            return Index(filteredList);
        }
        [Route("FilterByName")]
        [HttpGet]
        public ActionResult FilterByName(string searchText)
        {
            IEnumerable<IClient> filteredList = _clientService.FilterClientsByName(searchText);
            return Index(filteredList);
        }
    }
}