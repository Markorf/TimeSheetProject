using System.Data;
using System.Data.SqlClient;

namespace TimeSheet.DAL.Repositories.DbService.Interfaces
{
    public interface IDbService
    {
        IDbConnectionService DbConnectionService { get; set; }
        SqlConnection CreateDbConnection();
    }
}
