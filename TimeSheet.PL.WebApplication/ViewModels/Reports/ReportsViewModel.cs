using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.PL.WebApplication.ViewModels.Shared;

namespace TimeSheet.PL.WebApplication.ViewModels.Reports
{
    public class ReportsViewModel : LayoutViewModel
    {
        public ReportsViewModel()
        {
            HeaderViewModel.NavbarViewModel.ActiveItem = NavbarViewModel.NavItems.Reports;
        }
    }
}