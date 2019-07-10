using System;
using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.Repository.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using System.Data;
using System.Data.SqlClient;

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
            List<IClient> clientList = new List<IClient>();
            using (SqlConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Client;", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clientList.Add(new Client(GetSafeString(reader, 1), GetSafeString(reader, 2), GetSafeString(reader, 3), GetSafeString(reader, 4), GetSafeGuid(reader, 5)));
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
                    command.Parameters.Add(command.CreateParameter("@id", client.Id));
                    command.Parameters.Add(command.CreateParameter("@name", client.Name));
                    command.Parameters.Add(command.CreateParameter("@Address", PassAppropriateVal(client.Address)));
                    command.Parameters.Add(command.CreateParameter("@city", PassAppropriateVal(client.City)));
                    command.Parameters.Add(command.CreateParameter("@zipCode", PassAppropriateVal(client.ZipCode)));
                    command.Parameters.Add(command.CreateParameter("@countryId", PassAppropriateVal(client.CountryId)));

                    command.ExecuteNonQuery();
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
                    command.AddCommand("DELETE FROM Client WHERE id=@id");
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
                    command.Parameters.Add(command.CreateParameter("@id", newClient.Id));
                    command.Parameters.Add(command.CreateParameter("@name", newClient.Name));
                    command.Parameters.Add(command.CreateParameter("@Address", PassAppropriateVal(newClient.Address)));
                    command.Parameters.Add(command.CreateParameter("@city", PassAppropriateVal(newClient.City)));
                    command.Parameters.Add(command.CreateParameter("@zipCode", PassAppropriateVal(newClient.ZipCode)));
                    command.Parameters.Add(command.CreateParameter("@countryId", PassAppropriateVal(newClient.CountryId)));

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<IClient> FilterClientsByName(string clientName)
        {
            List<IClient> clientList = new List<IClient>();
            using (SqlConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE Name LIKE @name;", connection);
                command.Parameters.Add(command.CreateParameter("@name", $"%{clientName}"));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clientList.Add(new Client(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetGuid(5)));
                }
            }
            return clientList;
        }

        public IEnumerable<IClient> FilterClientsByFirstLetter(char firstLetter)
        {
            List<IClient> clientList = new List<IClient>();
            using (SqlConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE Name LIKE @name;", connection);
                command.Parameters.Add(command.CreateParameter("@name", $"%{firstLetter}%"));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    clientList.Add(new Client(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetGuid(5)));
                }
            }
            return clientList;
        }

        private string GetSafeString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        private Guid GetSafeGuid(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetGuid(colIndex);
            return Guid.Empty;
        }

        private object PassAppropriateVal(object val)
            => val == null ? DBNull.Value : val;

    }
}

