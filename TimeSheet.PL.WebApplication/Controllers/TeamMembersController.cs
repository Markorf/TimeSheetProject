using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.PL.WebApp.Controllers
{
    public class TeamMembersController : Controller
    {
        [Route("team-members")]
        public ActionResult Index()
        {
            return View();
        }
    }
}