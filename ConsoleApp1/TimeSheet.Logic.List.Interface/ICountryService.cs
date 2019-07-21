using System.Collections.Generic;
using TimeSheet.Shared.Models.Interfaces;
using TimeSheet.DAL.Repositories.Repository.Interfaces;

namespace TimeSheet.BLL.Service.Interfaces
{
    public interface ICountryService
    {
        ICountryDAL CountryDAL { get; }
        IEnumerable<ICountry> GetCountries();
    }
}
