using System;
using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.DAL.Repositories.List.Interfaces
{
    public interface IClientDAL
    {
        IEnumerable<IClient> GetClients();
        void UpdateClientById(IClient updatedClient);
        bool RemoveClientById(Guid clientId);
        void AddClient(IClient newClient);
        IEnumerable<IClient> FilterClientsByName(string clientName);
        IEnumerable<IClient> FilterClientsByFirstLetter(char firstLetter);
    }
}

