using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Interfaces;

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

                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new KeyNotFoundException("Client not found");
                    }

                }
            }
        }

        public bool RemoveClientById(Guid id)
        {
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("DELETE FROM Client WHERE Id=@id;");
                    command.Parameters.Add(command.CreateParameter("@id", id));
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public IClient GetClientById(Guid id)
        {
            IClient client = null;
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("SELECT * FROM Client WHERE Id = @id");
                    command.Parameters.Add(command.CreateParameter("@id", id));

                    using (IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (dataReader.Read())
                        {
                            client = MapClient(dataReader);
                        }
                        else
                        {
                            throw new KeyNotFoundException("Client not found");
                        }

                    }
                }
            }

            return client;
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
                    catch (SqlException ex)
                    {
                        throw new ConstraintException(ex.Message);
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
                    command.AddCommand("SELECT * FROM Client WHERE lower(Name) LIKE @name;");
                    command.Parameters.Add(command.CreateParameter("@name", $"%{clientName.ToLower()}%"));
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
                    command.AddCommand("SELECT * FROM Client WHERE lower(Name) LIKE @name;");
                    command.Parameters.Add(command.CreateParameter("@name", $"{firstLetter.ToString().ToLower()}%"));

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

        public IEnumerable<IClient> GetClientsByPaging(int offset, int rowsCount)
        {
            List<IClient> clientList = new List<IClient>();
            using (IDbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("SELECT * FROM Client ORDER BY Name OFFSET @offset ROWS FETCH NEXT @rowsCount ROWS ONLY;");
                    command.Parameters.Add(command.CreateParameter("@offset", offset));
                    command.Parameters.Add(command.CreateParameter("@rowsCount", rowsCount));

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

