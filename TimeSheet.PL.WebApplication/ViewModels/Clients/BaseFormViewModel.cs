using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.PL.WebApplication.ViewModels.Shared;
using TimeSheet.Shared.Models.Implementation;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class BaseFormViewModel
    {
        public IEnumerable<ICountry> Countries { get; set; }
        public IClient Client { get; set; } = new UpdateClientViewModel();
    }
}