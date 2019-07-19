using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.DAL.Repositories.Repository.Implementation;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using System.Data;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    [TestClass]
    public class ClientDALTests
    {
        private string _connectionStringName = "Connection";
        private DbSeeder _dbSeeder;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbSeeder = new DbSeeder(_connectionStringName);
            _dbSeeder.ResetTables();
        }

        [TestMethod]
        public void ClientDAL_InitWithValidDBService_ReturnsInstance()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            Assert.IsTrue(clientDAL != null);
        }

        [TestMethod]
        public void ClientDAL_InitWithNullDBService_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ClientDAL(null));
        }

        [TestMethod]
        public void GetClients_GetClientsFromNonEmptyTable_ReturnsTableWithList()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            List<IClient> clientList = clientDAL.GetClients().ToList();
            Assert.IsTrue(clientList.Count() == DbSeeder.clientList.Count());
        }

        [TestMethod]
        public void GetClientsByPaging_GetClientsFromNonEmptyTable_ReturnsTableWithList()
        {
            int rows = 5;
            int offset = 0;
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            List<IClient> clientList = clientDAL.GetClientsByPaging(offset, rows).ToList();
            Assert.IsTrue(clientList.Count() <= rows && clientList.Count() > 0);
        }

        [TestMethod]
        public void GetClients_GetClientsFromEmptyTable_ReturnsEmptyTable()
        {
            _dbSeeder.EmptyTables();
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IEnumerable<IClient> clientList = clientDAL.GetClients();
            Assert.IsTrue(clientList.Count() == 0);
        }

        [TestMethod]
        public void GetClientsByPaging_GetClientsFromEmptyTable_ReturnsTableWithList()
        {
            _dbSeeder.EmptyTables();
            int rows = 5;
            int offset = 0;
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            List<IClient> clientList = clientDAL.GetClientsByPaging(offset, rows).ToList();
            Assert.IsTrue(clientList.Count() == 0);
        }

        [TestMethod]
        public void UpdateClientById_PassValidClientObject_ReturnsTableWithUpdatedClient()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IClient newClient = new Client(DbSeeder.TestClientId, "Foo", "Addr");
            clientDAL.UpdateClientById(newClient);
            List<IClient> updatedList = clientDAL.GetClients().ToList();
            IClient updatedClient = updatedList.Find(client => client.Id == DbSeeder.TestClientId);
            Assert.IsTrue(updatedClient.Name == "Foo" && updatedClient.Address == "Addr");
        }

        [TestMethod]
        public void UpdateClientById_PassClientWithInvalidCountryId_ThrowsKeyNotFoundException()
        {
            _dbSeeder.EmptyTables();
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            Assert.ThrowsException<KeyNotFoundException>(() => clientDAL.UpdateClientById(new Client(DbSeeder.TestClientId, "Foo", "Addr", null, null, Guid.NewGuid())));
        }

        [TestMethod]
        public void UpdateClientById_PassInvalidClientObject_ThrowsKeyNotFoundException()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IClient newClient = new Client(Guid.NewGuid(), "Foo", "Addr");
            Assert.ThrowsException<KeyNotFoundException>(() => clientDAL.UpdateClientById(newClient));
        }

        [TestMethod]
        public void RemoveClientById_PassExistingClientId_ReturnsTableWithoutDeletedClient()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IEnumerable<IClient> clientList = clientDAL.GetClients();
            int oldCount = clientList.Count();
            bool isClientDeleted = clientDAL.RemoveClientById(DbSeeder.TestClientId);
            int newCount = clientDAL.GetClients().Count();
            Assert.IsTrue(isClientDeleted && newCount == oldCount - 1);
        }

        [TestMethod]
        public void RemoveClientById_PassNonExistingClientId_ReturnsSameTable()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IEnumerable<IClient> clientList = clientDAL.GetClients();
            int oldCount = clientList.Count();
            bool isClientDeleted = clientDAL.RemoveClientById(Guid.NewGuid());
            int newCount = clientDAL.GetClients().Count();
            Assert.IsTrue(!isClientDeleted && newCount == oldCount);
        }

        [TestMethod]
        public void GetClientById_PassValidGuid_ReturnsClientInstance()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IClient client = clientDAL.GetClientById(DbSeeder.TestClientId);
            Assert.IsTrue(client != null);
        }

        [TestMethod]
        public void GetClientById_PassNewGuid_ThrowsKeyNotFoundException()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            Assert.ThrowsException<KeyNotFoundException>(() => clientDAL.GetClientById(Guid.NewGuid()));
        }

        [TestMethod]
        public void AddClient_PassNewClient_ReturnsTableWithAddedClient()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IEnumerable<IClient> clientList = clientDAL.GetClients();
            int oldCount = clientList.Count();
            Guid id = Guid.NewGuid();
            clientDAL.AddClient(new Client(id, "Namex"));
            List<IClient> newList = clientDAL.GetClients().ToList();
            bool isNewClientFound = newList.FindIndex(client => client.Id == id) > -1;
            Assert.IsTrue(isNewClientFound && newList.Count() == oldCount + 1);
        }

        [TestMethod]
        public void AddClient_PassClientWithSameId_ThrowsException()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            Assert.ThrowsException<ConstraintException>(() => clientDAL.AddClient(new Client(DbSeeder.TestClientId, "Namex")));
        }

        [TestMethod]
        public void AddClient_PassNonExistingCountryId_ThrowsException()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            Assert.ThrowsException<ConstraintException>(() => clientDAL.AddClient(new Client(Guid.NewGuid(), "Namex", null, null, null, Guid.NewGuid())));
        }

        [TestMethod]
        public void FilterClientsByName_PassExistingClientName_ReturnsTableFiltSearchedClient()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            List<IClient> filteredList = clientDAL.FilterClientsByName(DbSeeder.TestClientName).ToList();
            bool isClientInList = filteredList.FindIndex(client => client.Name == DbSeeder.TestClientName) > -1;
            Assert.IsTrue(isClientInList);
        }

        [TestMethod]
        public void FilterClientsByName_PassNonExistingClientName_ReturnsEmptyTable()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IEnumerable<IClient> filteredList = clientDAL.FilterClientsByName("NonExistingName");
            Assert.IsTrue(filteredList.Count() == 0);
        }

        [TestMethod]
        public void FilterClientsFirstLetter_PassExistingFirstLetter_ReturnsTableFiltSearchedClient()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            List<IClient> filteredList = clientDAL.FilterClientsByFirstLetter(DbSeeder.TestClientName[0]).ToList();
            bool isClientInList = filteredList.FindIndex(client => client.Name == DbSeeder.TestClientName) > -1;
            Assert.IsTrue(isClientInList);
        }

        [TestMethod]
        public void FilterClientsByName_PassNonExistingClientFirstLetter_ReturnsEmptyTable()
        {
            IClientDAL clientDAL = new ClientDAL(new DBService(new DbConnectionService(_connectionStringName)));
            IEnumerable<IClient> filteredList = clientDAL.FilterClientsByFirstLetter(']');
            Assert.IsTrue(filteredList.Count() == 0);
        }
    }
}
