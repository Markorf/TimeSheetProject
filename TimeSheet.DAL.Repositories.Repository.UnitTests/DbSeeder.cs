using TimeSheet.DAL.Repositories.DbService.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.Repository.Implementation;
using System.Data;
using TimeSheet.DAL.Repositories.Database.Implementation;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    public class DbSeeder
    {
        private IDbService _dbService;

        public DbSeeder(string connectionString)
        {
            _dbService = new DBService(new DbConnectionService("Connection"));
        }

        public void PopulateCountryTable()
        {
            RemoveDataFromTable("DELETE * Country");
            AddDataToTable("INSERT INTO [dbo].[Country] ([Id], [Name]) VALUES (N'cb77cce6-c2cb-471b-bdd3-5dac8c93b756', N'Colombia')" +
                            "INSERT INTO[dbo].[Country]([Id], [Name]) VALUES(N'cb77cce5-c2cb-471b-bdd4-5dac8c93b756', N'Russia')" +
                            "INSERT INTO[dbo].[Country]([Id], [Name]) VALUES(N'cb77cce6-c2cb-471b-bdd4-5dac8c93b756', N'Zambia')");

        }

        public void PopulateClientTable()
        {
            RemoveDataFromTable("DELETE * Client");
            AddDataToTable("INSERT INTO [dbo].[Client] ([Id], [Name], [Address], [City], [ZipCode], [CountryId]) VALUES (N'cb77cce6-c2cb-471b-bdd3-5dac8c93b751', N'Max', N'Addr', N'Ct', N'21312', N'cb77cce6-c2cb-471b-bdd3-5dac8c93b756')" +
                            "INSERT INTO[dbo].[Client]([Id], [Name], [Address], [City], [ZipCode], [CountryId]) VALUES(N'cb77cce6-c2cb-471b-bdd3-5dac8c93b750', N'Bob', NULL, NULL, NULL, N'cb77cce6-c2cb-471b-bdd3-5dac8c93b756')" +
                            "INSERT INTO[dbo].[Client]([Id], [Name], [Address], [City], [ZipCode], [CountryId]) VALUES(N'cb77cce6-c2cb-471b-bdd3-5dac8c93b752', N'Johny', NULL, NULL, NULL, NULL)");
        }

        private void RemoveDataFromTable(string sql)
        {
            using (IDbConnection connection = _dbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand(sql);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void AddDataToTable(string sql)
        {
            using (IDbConnection connection = _dbService.CreateDbConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.AddCommand(sql);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
