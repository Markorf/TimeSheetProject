using System.Data;

namespace TimeSheet.DAL.Repositories.DbService.Interfaces
{
    public interface IDbService
    {
        IDbConnectionService DbConnectionService { get; set; }
        IDbConnection CreateDbConnection();
    }
}
