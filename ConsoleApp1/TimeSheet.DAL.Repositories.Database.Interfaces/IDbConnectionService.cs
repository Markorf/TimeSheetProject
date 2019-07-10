using System.Configuration;

namespace TimeSheet.DAL.Repositories.DbService.Interfaces
{
    public interface IDbConnectionService
    {
        ConnectionStringSettings GetConnectionSettings();
    }
}
