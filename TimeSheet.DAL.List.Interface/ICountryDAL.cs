using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.DAL.Repositories.List.Interfaces
{
    public interface ICountryDAL
    {
        IEnumerable<ICountry> GetCountries();
    }
}
