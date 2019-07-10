using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.DAL.Repositories.Repository.Interfaces
{
    public interface ICountryDAL
    {
        IEnumerable<ICountry> GetCountries();
    }
}
