using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.PL.WebApplication.ViewModels.Shared;

namespace TimeSheet.PL.WebApplication.ViewModels.Projects
{
    public class ProjectsViewModel : LayoutViewModel
    {
        public ProjectsViewModel()
        {
            HeaderViewModel.NavbarViewModel.ActiveItem = NavbarViewModel.NavItems.Projects;
        }
    }
}