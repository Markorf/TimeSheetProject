using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetLib
{
    public class ClientService
    {
        private List<Client> ClientList = new List<Client>() {
                new Client(1, "Marko", "Romanijska", "Futog", "21222"),
                new Client(2, "Pera", "Povrtarska", "Kikinda", "212"),
                new Client(3, "Jeca", "Krajiska", "Zrenjanin", "23000"),
            };
        public List<Client> GetClients()
                => ClientList;

        public void EditClientById(int id, Client updatedClient)
        {
            if (updatedClient == null)
            {
                return;
            }
            int clientIndex = ClientList.FindIndex(client => client.Id == id);
            if (clientIndex == -1)
            {
                return;
            }
            ClientList[clientIndex] = updatedClient;
        }
        public void RemoveClientById(int id)
        {
            int clientIndex = ClientList.FindIndex(client => client.Id == id);
            if (clientIndex != -1)
            {
               ClientList.RemoveAt(clientIndex);
            }
            return;
        }
        public void AddClient(Client newClient)
        {
            if (newClient != null)
            {
                ClientList.Add(newClient);
            }
            return;

        }
        public List<Client> FilterClientsByName(string clientName)
        {
            List<Client> allClients = GetClients();
            if (clientName == null)
            {
                return new List<Client>() {};
            }
            List<Client> filteredClients = allClients.Where(client => client.ClientName.Contains(clientName)).ToList();
            return filteredClients;
        }
    }
}
