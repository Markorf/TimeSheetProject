using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.PL.WebApplication.ViewModels.Projects;

namespace TimeSheet.PL.WebApp.Controllers
{
    public class ProjectsController : Controller
    {
        [Route("projects")]
        public ActionResult Index()
        {
            return View(new ProjectsViewModel());
        }
    }
}