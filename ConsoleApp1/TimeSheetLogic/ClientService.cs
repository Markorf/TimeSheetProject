using System;
using System.Collections.Generic;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.BLL.Service.Implementation
{
    public class ClientService : IClientService
    {
        public IClientDAL ClientDAL { get; }

        public ClientService(IClientDAL clientDAL)
        {
            if (clientDAL == null)
            {
                throw new ArgumentNullException("Value cannot be null", nameof(clientDAL));
            }
            ClientDAL = clientDAL;
        }

        public IEnumerable<IClient> GetClients()
          => ClientDAL.GetClients();

        public void UpdateClientById(IClient client)
        {
            Validate(client);
            ClientDAL.UpdateClientById(client);
        }

        public bool RemoveClientById(Guid id)
          => ClientDAL.RemoveClientById(id);


        public void AddClient(IClient newClient)
        {
            Validate(newClient);
            ClientDAL.AddClient(newClient);
        }

        public IEnumerable<IClient> FilterClientsByName(string clientName)
        {
            if (clientName == null)
            {
                throw new ArgumentNullException("Value cannot be null", nameof(clientName));
            }
            return ClientDAL.FilterClientsByName(clientName);
        }

        public IEnumerable<IClient> FilterClientsByFirstLetter(char firstLetter)
         => ClientDAL.FilterClientsByFirstLetter(firstLetter);

        private void Validate(IClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("Client can't be null", nameof(client));
            }
            if (String.IsNullOrEmpty(client.Name))
            {
                throw new Exception("Client name can't be null or empty string");
            }
        }
    }
}
