using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.BLL.Service.Implementation;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using NSubstitute;

namespace TimeSheetLogic.UnitTests
{
    [TestClass]
    public class ClientServiceTests
    {
        IClientDAL _clientDAL;
        List<IClient> _clientList;

        [TestInitialize]
        public void TestInitialize()
        {
            _clientDAL = Substitute.For<IClientDAL>();
            _clientList = new List<IClient>() { new Client("Marko"), new Client("Max"), new Client("Bob") };
        }

        [TestMethod]
        public void ClientService_InitWithNotNullDAL_ReturnsClientServiceInstance()
        {
            // act
            ClientService clientService = new ClientService(_clientDAL);
            // assert
            Assert.IsTrue(clientService != null);
        }

        [TestMethod]
        public void ClientService_InitWithNullInsteadDAL_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ClientService(null));
        }

        [TestMethod]
        public void GetClients_GetListWithClients_ReturnsNonEmptyList()
        {
            // Arrange
            _clientDAL.GetClients().Returns(_clientList);
            ClientService clientService = new ClientService(_clientDAL);
            // Act
            IEnumerable<IClient> clientList = clientService.GetClients();
            int listCount = clientList.Count();
            // Assert
            Assert.IsTrue(clientList.Count() == clientList.Count());
        }

        [TestMethod]
        public void GetClients_GetListWithNoClients_ReturnsEmptyList()
        {
            // Arrange
            _clientDAL.GetClients().Returns(new List<IClient>() { });
            ClientService clientService = new ClientService(_clientDAL);
            // Act
            IEnumerable<IClient> clientList = clientService.GetClients();
            // Assert
            Assert.IsTrue(clientList.Count() == 0);
        }

        [TestMethod]
        public void AddClient_AddNewClient_ReturnsTrue()
        {
            // Arrange
            ClientService clientService = new ClientService(_clientDAL);
            Client clientToAdd = new Client("Max", null, "Addr", "CITY", null);
            // Act
            clientService.AddClient(clientToAdd);
            // Assert
            _clientDAL.Received(1).AddClient(Arg.Is<IClient>(client => client.Name == "Max"));

        }

        [TestMethod]
        public void AddClient_AddNullInsteadClient_ThrowsArgumentNullException()
        {
            ClientService clientService = new ClientService(_clientDAL);
            Assert.ThrowsException<ArgumentNullException>(() => clientService.AddClient(null));
            _clientDAL.DidNotReceive().AddClient(Arg.Any<IClient>());
        }

        [TestMethod]
        public void AddClient_AddClientWithInvalidFields_ThrowsException()
        {
            ClientService clientService = new ClientService(_clientDAL);
            Assert.ThrowsException<ArgumentException>(() => clientService.AddClient(new Client(null)));
            _clientDAL.DidNotReceive().AddClient(Arg.Any<IClient>());
        }

        [TestMethod]
        public void RemoveClientById_RemoveClientWithExistingId_ReturnsTrue()
        {
            // Arrange
            _clientDAL.GetClients().Returns(_clientList);
            ClientService clientService = new ClientService(_clientDAL);
            List<IClient> clientList = clientService.GetClients().ToList();
            Guid clientId = clientList[0].Id;
            _clientDAL.RemoveClientById(clientId).Returns(true);
            // Act

            bool clientRemoved = clientService.RemoveClientById(clientId);

            // Assert
            Assert.IsTrue(clientRemoved);
            _clientDAL.Received(1).RemoveClientById(Arg.Is<Guid>(id => id == clientId));
        }

        [TestMethod]
        public void RemoveClientById_PassClientWithNonExistingId_ReturnsTrue()
        {
            // Arrange
            _clientDAL.GetClients().Returns(_clientList);
            ClientService clientService = new ClientService(_clientDAL);
            Guid clientId = Guid.NewGuid();
            _clientDAL.RemoveClientById(clientId).Returns(false);
            // Act
            bool isClientRemoved = clientService.RemoveClientById(clientId);
            // Assert

            Assert.IsFalse(isClientRemoved);
            _clientDAL.Received(1).RemoveClientById(Arg.Is<Guid>(id => id == clientId));
        }

        [TestMethod]
        public void FilterClientsByName_FilterClientsWithExistingName_ReturnsFilteredListWithValue()
        {
            // Arrange
            string clientName = "Marko";
            _clientDAL.FilterClientsByName(clientName).Returns(new List<IClient>() { new Client("Marko") });
            ClientService clientService = new ClientService(_clientDAL);

            // Act
            IEnumerable<IClient> filteredClients = clientService.FilterClientsByName(clientName);

            // Assert
            Assert.IsTrue(filteredClients.Count() == 1);
            _clientDAL.Received(1).FilterClientsByName(Arg.Is<string>(name => name == clientName));
        }

        [TestMethod]
        public void FilterClientsByName_FilterClientsWithNonExistingName_ReturnsEmptyClientList()
        {
            // Arrange
            ClientService clientService = new ClientService(_clientDAL);
            string searchText = "NonExistingName";
            _clientDAL.FilterClientsByName(searchText).Returns(new List<IClient>() { });

            // Act
            IEnumerable<IClient> filteredClients = clientService.FilterClientsByName(searchText);

            // Assert
            Assert.IsTrue(filteredClients.Count() == 0);
            _clientDAL.Received(1).FilterClientsByName(Arg.Is<string>(name => name == searchText));
        }

        [TestMethod]
        public void FilterClientsByName_FilterClientsWithNull_ThrowsArgumentNullException()
        {
            ClientService clientService = new ClientService(_clientDAL);
            string searchText = null;
            Assert.ThrowsException<ArgumentNullException>(() => clientService.FilterClientsByName(searchText));
            _clientDAL.DidNotReceive().FilterClientsByName(Arg.Any<string>());
        }

        [TestMethod]
        public void FilterClientsByName_FilterClientsWithEmptyString_ReturnsNonFilteredList()
        {
            string searchText = "";
            _clientDAL.FilterClientsByName(searchText).Returns(_clientList);
            _clientDAL.GetClients().Returns(_clientList);
            // Arrange
            ClientService clientService = new ClientService(_clientDAL);
            IEnumerable<IClient> clientList = clientService.GetClients();

            // Act
            IEnumerable<IClient> filteredClients = clientService.FilterClientsByName(searchText);

            // Assert
            Assert.IsTrue(filteredClients.Count() == clientList.Count());
            _clientDAL.Received(1).FilterClientsByName(Arg.Is<string>(name => name == searchText));
        }

        [TestMethod]
        public void UpdateClientById_PassExistingClient_ReturnsIsClientEdited()
        {
            // Arrange
            string clientName = "Maxx";
            ClientService clientService = new ClientService(_clientDAL);
            IClient newClient = new Client(clientName, null, "Addr", "City", Guid.NewGuid()) { Id = _clientList.First().Id };

            // Act
            clientService.UpdateClientById(newClient);
            // Assert
            _clientDAL.Received(1).UpdateClientById(Arg.Is<IClient>(client => client.Name == clientName));
        }

        [TestMethod]
        public void UpdateClientById_PassNull_ThrowsArgumentNullException()
        {
            ClientService clientService = new ClientService(_clientDAL);
            Assert.ThrowsException<ArgumentNullException>(() => clientService.UpdateClientById(null));
            _clientDAL.DidNotReceive().UpdateClientById(Arg.Any<IClient>());
        }

        [TestMethod]
        public void UpdateClientById_PassClientWithInvalidFields_ThrowsArgumentException()
        {
            ClientService clientService = new ClientService(_clientDAL);
            Assert.ThrowsException<ArgumentException>(() => clientService.UpdateClientById(new Client(null)));
            _clientDAL.DidNotReceive().UpdateClientById(Arg.Any<IClient>());
        }

        [TestMethod]
        public void FilterClientsByFirstLetter_FilterWithNonEmptyLetter_ReturnsCorrectList()
        {
            // Arrange
            _clientDAL.GetClients().Returns(_clientList);
            ClientService clientService = new ClientService(_clientDAL);
            IEnumerable<IClient> clientList = clientService.GetClients();
            char firstLetter = 'M';
            _clientDAL.FilterClientsByFirstLetter(firstLetter).Returns(new List<IClient>() { new Client("Marko"), new Client("Max") });

            // Act
            IEnumerable<IClient> filteredClientList = clientService.FilterClientsByFirstLetter(firstLetter);

            // Assert
            Assert.AreNotEqual(clientList, filteredClientList);
            _clientDAL.Received(1).FilterClientsByFirstLetter(Arg.Is<char>(firstChar => firstChar == firstLetter));
        }

        [TestMethod]
        public void FilterClientsByFirstLetter_FilterWithNonExistingLetterInList_ReturnsEmptyList()
        {
            // Arrange
            char firstLetter = '\0';
            _clientDAL.FilterClientsByFirstLetter(firstLetter).Returns(new List<IClient>() { });
            ClientService clientService = new ClientService(_clientDAL);

            // Act
            IEnumerable<IClient> filteredClientList = clientService.FilterClientsByFirstLetter(firstLetter);

            // Assert
            int clientListCount = filteredClientList.Count();
            Assert.IsTrue(clientListCount == 0);
            _clientDAL.Received(1).FilterClientsByFirstLetter(Arg.Is<char>(firstChar => firstChar == firstLetter));
        }
    }
}
