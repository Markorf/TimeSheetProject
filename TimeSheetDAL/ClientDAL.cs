using System;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.DAL.Repositories.List.Interfaces;
using TimeSheet.DAL.Repositories.Database.Interfaces;
using System.Data.Common;
using System.Data;

namespace TimeSheet.DAL.Repositories.List.Implementation
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
            DataSet ds = new DataSet("Client");
            using (DbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (DbDataAdapter dbDataAdapter = _DbService.CreateDbDataAdapter())
                {
                    (dbDataAdapter as IDbDataAdapter).SelectCommand = connection.CreateCommand("SELECT * FROM Client;");
                    dbDataAdapter.Fill(ds);
                }
            }
            return ds.Tables[0].AsEnumerable().Select(row => MapRowToClient(row));
        }

        public void UpdateClientById(IClient client)
        {
            using (DbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("Update Client SET Name=@name, Address=@address, City=@city, ZipCode=@zipCode WHERE id=@id");
                    command.Parameters.Add(command.CreateParameter("@id", client.Id));
                    command.Parameters.Add(command.CreateParameter("@name", client.Name));
                    command.Parameters.Add(command.CreateParameter("@Address", PassAppropriateVal(client.Address)));
                    command.Parameters.Add(command.CreateParameter("@city", PassAppropriateVal(client.City)));
                    command.Parameters.Add(command.CreateParameter("@zipCode", PassAppropriateVal(client.ZipCode)));

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool RemoveClientById(Guid clientId)
        {
            using (DbConnection connection = _DbService.CreateDbConnection())
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
            using (DbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand("INSERT INTO Client (Id, Name, Address, City, ZipCode) VALUES (@id, @name, @address, @city, @zipCode)");
                    command.Parameters.Add(command.CreateParameter("@id", newClient.Id));
                    command.Parameters.Add(command.CreateParameter("@name", newClient.Name));
                    command.Parameters.Add(command.CreateParameter("@Address", PassAppropriateVal(newClient.Address)));
                    command.Parameters.Add(command.CreateParameter("@city", PassAppropriateVal(newClient.City)));
                    command.Parameters.Add(command.CreateParameter("@zipCode", PassAppropriateVal(newClient.ZipCode)));

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<IClient> FilterClientsByName(string clientName)
        {
            DataSet ds = new DataSet("Client");
            using (DbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (DbDataAdapter dbDataAdapter = _DbService.CreateDbDataAdapter())
                {
                    (dbDataAdapter as IDbDataAdapter).SelectCommand = connection.CreateCommand("SELECT * FROM Client WHERE Name LIKE @name;");
                    dbDataAdapter.SelectCommand.Parameters.Add(dbDataAdapter.SelectCommand.CreateParameter("@name", $"%{clientName}%"));
                    dbDataAdapter.Fill(ds);
                }
            }
            return ds.Tables[0].AsEnumerable().Select(row => MapRowToClient(row));
        }

        public IEnumerable<IClient> FilterClientsByFirstLetter(char firstLetter)
        {
            DataSet ds = new DataSet("Client");
            using (DbConnection connection = _DbService.CreateDbConnection())
            {
                connection.Open();
                using (DbDataAdapter dbDataAdapter = _DbService.CreateDbDataAdapter())
                {
                    (dbDataAdapter as IDbDataAdapter).SelectCommand = connection.CreateCommand("SELECT * FROM Client WHERE Name LIKE @name;");
                    dbDataAdapter.SelectCommand.Parameters.Add(dbDataAdapter.SelectCommand.CreateParameter("@name", $"{firstLetter}%"));
                    dbDataAdapter.Fill(ds);
                }
            }
            return ds.Tables[0].AsEnumerable().Select(row => MapRowToClient(row));
        }

        private object PassAppropriateVal(object val)
            => val == null ? DBNull.Value : val;

        private IClient MapRowToClient(DataRow row)
            => new Client(row.Field<string>("Name"), row.Field<Guid>("countryId"), row.Field<string>("Address"), row.Field<string>("City"), row.Field<string>("ZipCode"));
    }
}
