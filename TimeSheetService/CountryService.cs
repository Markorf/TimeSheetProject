using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetLib
{
    public class CountryService
    {
        private List<Country> CountryList = new List<Country>() {
                  new Country(1, "Serbia"),
                  new Country(2, "Brazil"),
                  new Country(3, "Germany")
        };

        public List<Country> GetCountries()
          => CountryList;
    }
}
