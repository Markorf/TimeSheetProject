using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.PL.WebApplication.ViewModels.Team_members;

namespace TimeSheet.PL.WebApp.Controllers
{
    public class TeamMembersController : Controller
    {
        [Route("team-members")]
        public ActionResult Index()
        {
            return View(new TeamMembersViewModel());
        }
    }
}