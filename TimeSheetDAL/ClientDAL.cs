using System;
using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using System.Data;

namespace TimeSheet.DAL.Repositories.Repository.Implementation
{
    public class ClientDAL : IClientDAL
    {
        private IDbService _DbService;

        public ClientDAL(IDbService DbService)
        {
            if (DbService == null)
            {
                throw new ArgumentNullException("Value cannot be null");
            }
            _DbService = DbService;
        }

        public IEnumerable<IClient> GetClients()
        {
            List<IClient> clientList = new List<IClient>() { };
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("SELECT * FROM Client");

                    using (IDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            clientList.Add(MapClient(dataReader));
                        }

                    }
                }
            }
            return clientList;
        }

        public void UpdateClientById(IClient client)
        {
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("Update Client SET Name=@name, Address=@address, City=@city, ZipCode=@zipCode, CountryId=@countryId WHERE id=@id");
                    AddParameters(command, client);
                    try
                    {
                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Client not updated");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public bool RemoveClientById(Guid clientId)
        {
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("DELETE FROM Client WHERE Id=@id;");
                    command.Parameters.Add(command.CreateParameter("@id", clientId));
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public void AddClient(IClient newClient)
        {
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("INSERT INTO Client (Id, Name, Address, City, ZipCode, CountryId) VALUES (@id, @name, @address, @city, @zipCode, @countryId)");
                    AddParameters(command, newClient);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public IEnumerable<IClient> FilterClientsByName(string clientName)
        {
            List<IClient> clientList = new List<IClient>();
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("SELECT * FROM Client WHERE Name LIKE @name;");
                    command.Parameters.Add(command.CreateParameter("@name", $"%{clientName}"));
                    using (IDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            clientList.Add(MapClient(dataReader));
                        }

                    }
                }
            }
            return clientList;
        }

        public IEnumerable<IClient> FilterClientsByFirstLetter(char firstLetter)
        {
            List<IClient> clientList = new List<IClient>();
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("SELECT * FROM Client WHERE Name LIKE @name;");
                    command.Parameters.Add(command.CreateParameter("@name", $"%{firstLetter}%"));

                    using (IDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            clientList.Add(MapClient(dataReader));

                        }
                    }
                }
            }
            return clientList;
        }

        private IClient MapClient(IDataRecord dataRecord)
              => new Client(
                                dataRecord.GetSafeGuid(0),
                                dataRecord.GetSafeString(1),
                                dataRecord.GetSafeString(2),
                                dataRecord.GetSafeString(3),
                                dataRecord.GetSafeString(4),
                                dataRecord.GetSafeGuid(5)
                      );

        private void AddParameters(IDbCommand command, IClient client)
        {
            command.Parameters.Add(command.CreateParameter("@id", client.Id.GetDBNull()));
            command.Parameters.Add(command.CreateParameter("@name", client.Name.GetDBNull()));
            command.Parameters.Add(command.CreateParameter("@Address", client.Address.GetDBNull()));
            command.Parameters.Add(command.CreateParameter("@city", client.City.GetDBNull()));
            command.Parameters.Add(command.CreateParameter("@zipCode", client.ZipCode.GetDBNull()));
            command.Parameters.Add(command.CreateParameter("@countryId", client.CountryId.GetDBNull()));
        }
    }
}

