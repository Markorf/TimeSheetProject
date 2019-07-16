using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.PL.WebApplication.ViewModels.Shared;

namespace TimeSheet.PL.WebApplication.ViewModels.TimeSheet
{
    public class TimeSheetViewModel : LayoutViewModel
    {
        public TimeSheetViewModel()
        {
            HeaderViewModel.NavbarViewModel.ActiveItem = NavbarViewModel.NavItems.TimeSheet;
        }
    }
}