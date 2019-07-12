using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.PL.WebApplication.ViewModels.Shared
{
    public class LayoutViewModel
    {
        public string Title { get; set; } = "TimeSheet";
        public HeaderViewModel HeaderViewModel { get; } = new HeaderViewModel();
    }
}