using System;
using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.DAL.Repositories.Repository.Interfaces;

namespace TimeSheet.BLL.Service.Interfaces
{
    public interface IClientService
    {
        IClientDAL ClientDAL { get; }
        IEnumerable<IClient> GetClients();
        void UpdateClientById(IClient clientToEdit);
        bool RemoveClientById(Guid id);
        void AddClient(IClient newClient);
        IEnumerable<IClient> FilterClientsByName(string clientName);
        IEnumerable<IClient> FilterClientsByFirstLetter(char firstLetter);
    }
}