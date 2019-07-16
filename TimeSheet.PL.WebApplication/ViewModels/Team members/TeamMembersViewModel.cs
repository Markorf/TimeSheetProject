using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.PL.WebApplication.ViewModels.Shared;

namespace TimeSheet.PL.WebApplication.ViewModels.Team_members
{
    public class TeamMembersViewModel : LayoutViewModel
    {
        public TeamMembersViewModel()
        {
            HeaderViewModel.NavbarViewModel.ActiveItem = NavbarViewModel.NavItems.TeamMembers;
        }
    }
}