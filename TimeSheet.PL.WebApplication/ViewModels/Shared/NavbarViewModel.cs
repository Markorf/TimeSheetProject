using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.PL.WebApplication.ViewModels.Shared
{
    public class NavbarViewModel
    {
        public enum NavItems
        {
            [Display(Name = "TimeSheet")]
            TimeSheet,
            [Display(Name = "Clients")]
            Clients,
            [Display(Name = "Projects")]
            Projects,
            [Display(Name = "Categories")]
            Categories,
            [Display(Name = "Team members")]
            TeamMembers,
            [Display(Name = "Reports")]
            Reports
        }
        public IEnumerable<NavItems> ItemsList;
        public NavItems ActiveItem = NavItems.TimeSheet;

        public NavbarViewModel()
        {
            ItemsList = Enum.GetValues(typeof(NavItems)).Cast<NavItems>();
        }

        public string GetNavItemName(NavItems item)
        {
            DisplayAttribute navItemDisplayName = item.GetAttribute<DisplayAttribute>();
            return navItemDisplayName.Name;

        }

    }
}