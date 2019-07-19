using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.PL.WebApplication.ViewModels.Clients
{
    public class CreateClientDialogPartialViewModel : BaseFormViewModel
    {
        public bool HasValidationError { get; set; } = false;
    }
}