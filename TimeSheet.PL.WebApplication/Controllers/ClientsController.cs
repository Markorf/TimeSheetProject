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
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.BLL.Service.Implementation;

namespace TimeSheet.PL.WebApp.Controllers
{
    public class ClientsController : Controller
    {
        private IDbService _dbService;
        private ICountryService _countryService;
        private IClientService _clientService;
        private string _connectionStringName = "Connection";

        public ClientsController()
        {
            _dbService = new DBService(new DbConnectionService(_connectionStringName));
            _countryService = new CountryService(new CountryDAL(_dbService));
            _clientService = new ClientService(new ClientDAL(_dbService));
        }

        [Route("clients")]
        public ActionResult Index()
        {

            IEnumerable<ICountry> countries = _countryService.GetCountries();
            IEnumerable<IClient> clients = _clientService.GetClients();
            return View(new ClientsViewModel(new ClientFormViewModel(countries), clients));
        }

        [Route("clients")]
        [HttpPost]
        public ActionResult Update(Client client)
        {
            _clientService.UpdateClientById(client);
            return RedirectToAction("index");

        }

        [Route("clients/{id}")]
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            // iskoristiti posle
            bool isClientDeleted = _clientService.RemoveClientById(id);
            return RedirectToAction("Index");
        }

        [Route("create")]
        [HttpPost]
        public ActionResult Create(ClientFormViewModel clientFormVM)
        {
            IClient client = new Client(Guid.NewGuid(), clientFormVM.Client.Name, clientFormVM.Client.Address, clientFormVM.Client.City, clientFormVM.Client.ZipCode, clientFormVM.Client.CountryId);
            _clientService.AddClient(client);
            return RedirectToAction("index");
        }
    }
}